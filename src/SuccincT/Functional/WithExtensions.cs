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

        public static T Copy<T>(this T @object) where T : class
        {
            return TryCopy(@object).ValueOrDefault;
        }

        public static Option<T> TryCopy<T>(this T @object) where T : class
        {
            var cachedTypeInfo = GetCachedTypeInfo(typeof(T));

            // Create the new object using the most specialized constructor
            var constructorToUse = cachedTypeInfo.CachedPublicConstructors
                                                 .OrderByDescending(cc => cc.Parameters.Count)
                                                 .TryFirst();

            if (!constructorToUse.HasValue) return Option<T>.None();

            var sourceReadProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.WriteOnlyProperties);

            var constructorParameters = constructorToUse.Value.Parameters;

            var @params = constructorParameters
                          .Select(p => sourceReadProperties.TryFirst(x => AreLinked(x, p)))
                          .Where(x => x.HasValue)
                          .Select(x => x.Value)
                          .Select(sourceReadProperty => sourceReadProperty.GetValue(@object, null))
                          .ToArray();

            if (@params.Length != constructorParameters.Count) return Option<T>.None();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            // Overwrite properties on the new created object
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

        public static T With<T, TProps>(this T @object, TProps propertiesToUpdate)
            where T : class where TProps : class
        {
            return TryWith(@object, propertiesToUpdate).ValueOrDefault;
        }

        public static Option<T> TryWith<T, TProps>(this T @object, TProps propertiesToUpdate)
            where T : class where TProps : class
        {
            // Do nothing if `propertiesToUpdate` is null
            if (propertiesToUpdate == null) return Option<T>.None();

            var cachedTypeInfo = GetCachedTypeInfo(typeof(T));

            // Create the new object using the most specialized constructor based on props to update
            var sourceReadProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.WriteOnlyProperties).ToList();
            var updateProperties = typeof(TProps).GetRuntimeProperties().Where(x => x.CanRead).ToList();

            var constructorToUse = cachedTypeInfo.CachedPublicConstructors
                                                 .OrderByDescending(
                                                     cc => cc.Parameters.Count(
                                                         p => updateProperties.Any(ptu => AreLinked(ptu, p))))
                                                 .TryFirst();

            if (!constructorToUse.HasValue) return Option<T>.None();

            var constructorParameters = constructorToUse.Value.Parameters;

            var @params = constructorParameters
                          .Select(p =>
                          {
                              return updateProperties
                                     .TryFirst(ptu => AreLinked(ptu, p))
                                     .Match<Option<object>>()
                                     .Some().Do(ptu => ptu.GetValue(propertiesToUpdate, null).ToOption())
                                     .None().Do(() =>
                                     {
                                         return sourceReadProperties
                                                .TryFirst(sp => AreLinked(sp, p))
                                                .Match<Option<object>>()
                                                .Some().Do(sp => sp.GetValue(@object, null).ToOption())
                                                .None().Do(Option<object>.None)
                                                .Result();
                                     })
                                     .Result();
                          })
                          .Where(x => x.HasValue)
                          .Select(x => x.Value)
                          .ToArray();

            if (@params.Length != constructorParameters.Count) return Option<T>.None();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            // Overwrite properties from the previous/source object
            var destWriteProperties = cachedTypeInfo.Properties.Except(cachedTypeInfo.ReadOnlyProperties);

            var propertiesToOverwrite = sourceReadProperties
                                        .Select(sourceProperty =>
                                                    destWriteProperties.TryFirst(
                                                        destProperty => sourceProperty.Name == destProperty.Name))
                                        .Where(x => x.HasValue)
                                        .Select(x => x.Value);

            foreach (var propertyToOverwrite in propertiesToOverwrite)
            {
                CopyPropertyValue(@object, propertyToOverwrite, newObject);
            }

            // Overwrite properties from the `propertiesToUpdate`
            var tuplePropertiesToUpdate = updateProperties
                                          .Select(propToUpdate =>
                                                      (
                                                      SourceProp: sourceReadProperties.TryFirst(
                                                          sp => sp.Name == propToUpdate.Name),
                                                      PropToUpdate: propToUpdate
                                                      )
                                          )
                                          .Where(x => x.SourceProp.HasValue && x.SourceProp.Value.CanWrite)
                                          .Select(x => (x.SourceProp.Value, x.PropToUpdate));

            foreach (var (sourceProp, propToUpdate) in tuplePropertiesToUpdate)
            {
                CopyPropertyValue(propertiesToUpdate, propToUpdate, newObject, sourceProp);
            }

            return Option<T>.Some(newObject);
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

        private static bool AreLinked(MemberInfo memberInfo, ParameterInfo parameterInfo)
        {
            return string.Equals(memberInfo.Name, parameterInfo.Name, StringComparison.CurrentCultureIgnoreCase);
        }

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

        private class CachedTypeInfo
        {
            public List<CachedConstructorInfo> CachedPublicConstructors { get; }
            public List<PropertyInfo> Properties { get; }
            public List<PropertyInfo> ReadOnlyProperties { get; }
            public List<PropertyInfo> WriteOnlyProperties { get; }

            public CachedTypeInfo(Type type)
            {
                var typeInfo = type.GetTypeInfo();

                var cachedConstructors = typeInfo.DeclaredConstructors.Select(c => new CachedConstructorInfo(c)).ToList();
                CachedPublicConstructors = cachedConstructors
                                           .Where(cc => cc.Constructor.IsPublic && !cc.Constructor.IsStatic).ToList();

                Properties = type.GetRuntimeProperties().ToList();
                ReadOnlyProperties = Properties.Where(p => p.CanRead && !p.CanWrite).ToList();
                WriteOnlyProperties = Properties.Where(p => !p.CanRead && p.CanWrite).ToList();
            }
        }

        private class CachedConstructorInfo
        {
            public ConstructorInfo Constructor { get; }
            public List<ParameterInfo> Parameters { get; }

            public CachedConstructorInfo(ConstructorInfo constructorInfo)
            {
                Constructor = constructorInfo;
                Parameters = constructorInfo.GetParameters().ToList();
            }
        }
    }
}
