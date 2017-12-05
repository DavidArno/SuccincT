using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SuccincT.Options;

namespace SuccincT.Functional
{
    public static class WithExtensions
    {
        private static readonly Dictionary<string, CachedTypeInfo> CachedTypeInfos =
            new Dictionary<string, CachedTypeInfo>();

        public static Option<T> Copy<T>(this T @object) where T : class
        {
            var cachedTypeInfo = GetCachedTypeInfo(typeof(T));

            // Create the new object using the most specialized constructor
            var constructorToUse = cachedTypeInfo.CachedPublicConstructors
                                                 .OrderByDescending(cc => cc.Parameters.Count)
                                                 .TryFirst();

            if (!constructorToUse.HasValue) return Option<T>.None();

            var sourceReadProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.WriteOnlyProperties).ToList();
            var constructorParameters = constructorToUse.Value.Parameters;

            var constructorParameterValues =
                GetConstructorParameterValuesForCopy(@object, sourceReadProperties, constructorParameters);

            if (constructorParameterValues.Length != constructorParameters.Count) return Option<T>.None();

            var newObject = Activator.CreateInstance(typeof(T), constructorParameterValues) as T;
            var destWriteProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.ReadOnlyProperties);

            var propertiesToOverwrite = sourceReadProperties
                                        .Select(p => destWriteProperties.TryFirst(x => p.Name == x.Name))
                                        .Where(x => x.HasValue)
                                        .Select(x => x.Value);

            foreach (var propertyToOverwrite in propertiesToOverwrite)
            {
                CopyPropertyValue(@object, propertyToOverwrite, newObject);
            }

            return Option<T>.Some(newObject);
        }

        private static object[] GetConstructorParameterValuesForCopy<T>(T @object,
                                                                        IEnumerable<PropertyInfo> sourceReadProperties,
                                                                        List<ParameterInfo> constructorParameters)
        {
            return constructorParameters
                   .Select(p => sourceReadProperties.TryFirst(x => AreLinked(x, p)))
                   .Where(x => x.HasValue)
                   .Select(x => x.Value)
                   .Select(sourceReadProperty => sourceReadProperty.GetValue(@object, null))
                   .ToArray();
        }

        public static Option<T> TryWith<T, TProps>(this T itemToCopy, TProps propertiesToUpdate)
            where T : class where TProps : class
        {
            if (propertiesToUpdate == null) return Option<T>.None();

            var cachedTypeInfo = GetCachedTypeInfo(typeof(T));
            var sourceReadProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.WriteOnlyProperties).ToList();
            var updateProperties = typeof(TProps).GetRuntimeProperties().Where(x => x.CanRead).ToList();
            var constructorToUse = ConstructorToUseForWith(cachedTypeInfo, updateProperties, sourceReadProperties);

            if (!constructorToUse.HasValue) return Option<T>.None();

            var constructorParameters = constructorToUse.Value.Parameters;
            var constructorParameterValues = MapUpdateValuesToConstructorParameters(itemToCopy,
                                                                                    propertiesToUpdate,
                                                                                    constructorParameters,
                                                                                    updateProperties,
                                                                                    sourceReadProperties);

            var destWriteProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.ReadOnlyProperties);
            var propsToSetFromUpdateData = GetPropertiesToSetFromUpdateData(updateProperties,
                                                                            constructorParameters,
                                                                            sourceReadProperties);

            var propsToSetFromSourceObject = GetPropertiesToSetFromSourceObject(sourceReadProperties,
                                                                                constructorParameters,
                                                                                propsToSetFromUpdateData,
                                                                                destWriteProperties);

            try
            {
                return Option<T>.Some(CreateNewObjectApplyingUpdates(itemToCopy,
                                                                     propertiesToUpdate,
                                                                     constructorParameterValues,
                                                                     propsToSetFromSourceObject,
                                                                     propsToSetFromUpdateData));
            }
            catch (Exception)
            {
                return Option<T>.None();
            }
        }

        public static T With<T, TProps>(this T itemToCopy, TProps propertiesToUpdate)
            where T : class where TProps : class
        {
            if (propertiesToUpdate == null) throw new ArgumentNullException(nameof(propertiesToUpdate));

            var cachedTypeInfo = GetCachedTypeInfo(typeof(T));
            var sourceReadProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.WriteOnlyProperties).ToList();
            var updateProperties = typeof(TProps).GetRuntimeProperties().Where(x => x.CanRead).ToList();
            var constructorToUse = ConstructorToUseForWith(cachedTypeInfo, updateProperties, sourceReadProperties);

            if (!constructorToUse.HasValue) throw new CopyException(
                $"Type {typeof(T).Name} does not supply a suitable constructor for use with With, which allows all " +
                "non-writable properties to be set via that constructor.");

            var constructorParameters = constructorToUse.Value.Parameters;
            var constructorParameterValues = MapUpdateValuesToConstructorParameters(itemToCopy,
                                                                                    propertiesToUpdate,
                                                                                    constructorParameters,
                                                                                    updateProperties,
                                                                                    sourceReadProperties);

            var destWriteProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.ReadOnlyProperties);
            var propsToSetFromUpdateData = GetPropertiesToSetFromUpdateData(updateProperties,
                                                                            constructorParameters,
                                                                            sourceReadProperties);

            var propsToSetFromSourceObject = GetPropertiesToSetFromSourceObject(sourceReadProperties,
                                                                                constructorParameters,
                                                                                propsToSetFromUpdateData,
                                                                                destWriteProperties);

            try
            {
                return CreateNewObjectApplyingUpdates(itemToCopy,
                                                      propertiesToUpdate,
                                                      constructorParameterValues,
                                                      propsToSetFromSourceObject,
                                                      propsToSetFromUpdateData);
            }
            catch (Exception ex)
            {
                throw new CopyException($"A problem occurred creating a new instance of {typeof(T).Name} using With." +
                                        "See the inner exception for details of the problem.",
                                        ex);
            }
        }

        private static T CreateNewObjectApplyingUpdates<T, TProps>(T itemToCopy,
                                                                   TProps propertiesToUpdate,
                                                                   object[] constructorParameterValues,
                                                                   IEnumerable<PropertyInfo> propsToSetFromSourceObject,
                                                                   IEnumerable<(PropertyInfo Value, PropertyInfo PropToUpdate)> propsToSetFromUpdateData)
            where T : class where TProps : class
        {
            var newObject = Activator.CreateInstance(typeof(T), constructorParameterValues) as T;

            foreach (var propertyToOverwrite in propsToSetFromSourceObject)
            {
                CopyPropertyValue(itemToCopy, propertyToOverwrite, newObject);
            }

            foreach (var (sourceProp, propToUpdate) in propsToSetFromUpdateData)
            {
                CopyPropertyValue(propertiesToUpdate, propToUpdate, newObject, sourceProp);
            }

            return newObject;
        }

        private static List<PropertyInfo> GetPropertiesToSetFromSourceObject(
            IEnumerable<PropertyInfo> sourceReadProperties,
            List<ParameterInfo> constructorParameters,
            List<(PropertyInfo Value, PropertyInfo PropToUpdate)> propsToSetFromUpdateData,
            IEnumerable<PropertyInfo> destWriteProperties)
        {
            return sourceReadProperties
                   .Where(p => !constructorParameters.Any(cp => AreLinked(cp, p)))
                   .Where(p => !propsToSetFromUpdateData.Any(tp => AreLinked(p, tp.PropToUpdate)))
                   .Select(sourceProperty =>
                               destWriteProperties.TryFirst(
                                   destProperty => AreLinked(sourceProperty, destProperty)))
                   .Where(x => x.HasValue)
                   .Select(x => x.Value).ToList();
        }

        private static List<(PropertyInfo Value, PropertyInfo PropToUpdate)> GetPropertiesToSetFromUpdateData(
            IEnumerable<PropertyInfo> updateProperties,
            List<ParameterInfo> constructorParameters,
            List<PropertyInfo> sourceReadProperties)
        {
            return updateProperties
                   .Where(p => !constructorParameters.Any(cp => AreLinked(cp, p)))
                   .Select(propToUpdate =>
                               (
                               SourceProp: sourceReadProperties.TryFirst(
                                   sp => AreLinked(sp, propToUpdate)),
                               PropToUpdate: propToUpdate
                               )
                   )
                   .Where(x => x.SourceProp.HasValue && x.SourceProp.Value.CanWrite)
                   .Select(x => (x.SourceProp.Value, x.PropToUpdate)).ToList();
        }

        private static object[] MapUpdateValuesToConstructorParameters<T, TProps>(
            T @object,
            TProps propertiesToUpdate,
            List<ParameterInfo> constructorParameters,
            List<PropertyInfo> updateProperties,
            List<PropertyInfo> sourceReadProperties) where T : class where TProps : class
        {
            return constructorParameters
                   .Select(p =>
                   {
                       return updateProperties
                              .TryFirst(ptu => AreLinked(ptu, p))
                              .Match<Option<object>>()
                              .Some().Do(ptu => Option<object>.Some(ptu.GetValue(propertiesToUpdate, null)))
                              .None().Do(() =>
                              {
                                  return sourceReadProperties
                                         .TryFirst(sp => AreLinked(sp, p))
                                         .Match<Option<object>>()
                                         .Some().Do(sp => Option<object>.Some(sp.GetValue(@object, null)))
                                         .None().Do(Option<object>.None)
                                         .Result();
                              })
                              .Result();
                   })
                   .Where(x => x.HasValue)
                   .Select(x => x.Value)
                   .ToArray();
        }

        private static Option<CachedConstructorInfo> ConstructorToUseForWith(CachedTypeInfo cachedTypeInfo,
                                                                             IEnumerable<PropertyInfo> updateProperties,
                                                                             IEnumerable<PropertyInfo> readProperties)
        {
            return (from constructor in cachedTypeInfo.CachedPublicConstructors
                    let paramsNotCoveredByUpdates =
                        constructor.Parameters.Where(p => !updateProperties.Any(ptu => AreLinked(ptu, p)))
                    let remainingParamsNotCoveredByProperties =
                        paramsNotCoveredByUpdates.Where(p => !readProperties.Any(rp => AreLinked(rp, p))).ToList()
                    where !remainingParamsNotCoveredByProperties.Any()
                    orderby constructor.Parameters.Count descending
                    select constructor).TryFirst();
        }

        private static CachedTypeInfo GetCachedTypeInfo(Type type)
        {
            return CachedTypeInfos.GetOrAddValue(type.FullName, () => new CachedTypeInfo(type));
        }

        private static T GetOrAddValue<T>(this Dictionary<string, T> dictionary, string key, Func<T> createValue)
        {
            return dictionary.TryGetValue(key)
                             .Match<T>()
                             .Some().Do(value => value)
                             .None().Do(() =>
                             {
                                 var value = createValue();
                                 dictionary.Add(key, value);
                                 return value;
                             })
                             .Result();
        }

        private static bool AreLinked(MemberInfo memberInfo, ParameterInfo parameterInfo) =>
            string.Equals(memberInfo.Name, parameterInfo.Name, StringComparison.CurrentCultureIgnoreCase);

        private static bool AreLinked(MemberInfo memberInfo, PropertyInfo propertyInfo) =>
            string.Equals(memberInfo.Name, propertyInfo.Name, StringComparison.CurrentCultureIgnoreCase);

        private static bool AreLinked(ParameterInfo parameterInfo, PropertyInfo propertyInfo) =>
            string.Equals(parameterInfo.Name, propertyInfo.Name, StringComparison.CurrentCultureIgnoreCase);

        private static void CopyPropertyValue<T>(T from, PropertyInfo property, T to) where T : class
        {
            property.SetValue(to, property.GetValue(from, null));
        }

        private static void CopyPropertyValue<T1, T2>(T1 from,
                                                      PropertyInfo fromProperty,
                                                      T2 to,
                                                      PropertyInfo toProperty) where T1 : class where T2 : class
        {
            toProperty.SetValue(to, fromProperty.GetValue(from, null));
        }
    }
}
