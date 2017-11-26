using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SuccincT.Options;

namespace SuccincT.Functional
{
    public static class WithExtensions
    {
        public static Option<T> Copy<T>(this T @object) where T : class
        {
            // Create the new object using the most specialized constructor
            var constructorToUse = GetListOfPublicContructorInfo(typeof(T))
                .OrderByDescending(c => c.GetParameters().Length)
                .TryFirst();

            if (!constructorToUse.HasValue)
                return Option<T>.None();

            var sourceReadProperties = GetListOfReadPropertyInfo(typeof(T));

            var constructorParameters = constructorToUse.Value.GetParameters();

            var @params = constructorParameters
                .Select(p =>
                {
                    return sourceReadProperties
                        .TryFirst(sourceReadProperty => AreLinked(sourceReadProperty, p));
                })
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .Select(sourceReadProperty => sourceReadProperty.GetValue(@object, null))
                .ToArray();

            if (@params.Length != constructorParameters.Length)
                return Option<T>.None();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            // Overwrite properties on the new created object
            var destWriteProperties = GetListOfWritePropertyInfo(typeof(T));

            var propertiesToOverwrite = sourceReadProperties
                .Select(sourceProperty => destWriteProperties.TryFirst(destProperty => sourceProperty.Name == destProperty.Name))
                .Where(x => x.HasValue)
                .Select(x => x.Value);

            foreach (var propertyToOverwrite in propertiesToOverwrite)
            {
                CopyPropertyValue(@object, propertyToOverwrite, newObject);
            }

            return Option<T>.Some(newObject);
        }

        public static Option<T> With<T, TProps>(this T @object, TProps propertiesToUpdate) where T : class where TProps : class
        {
            // Do nothing if `propertiesToUpdate` is null
            if (propertiesToUpdate == null)
                return Option<T>.None();

            // Create the new object using the most specialized constructor based on props to update
            var sourceReadProperties = GetListOfReadPropertyInfo(typeof(T));
            var updateProperties = GetListOfReadPropertyInfo(typeof(TProps));

            var constructorToUse = GetListOfPublicContructorInfo(typeof(T))
                .OrderByDescending(c => c.GetParameters().Count(p => updateProperties.Any(ptu => AreLinked(ptu, p))))
                .TryFirst();

            if (!constructorToUse.HasValue)
                return Option<T>.None();

            var constructorParameters = constructorToUse.Value.GetParameters();

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

            if (@params.Length != constructorParameters.Length)
                return Option<T>.None();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            // Overwrite properties from the previous/source object
            var destWriteProperties = GetListOfWritePropertyInfo(typeof(T));

            var propertiesToOverwrite = sourceReadProperties
                .Select(sourceProperty => destWriteProperties.TryFirst(destProperty => sourceProperty.Name == destProperty.Name))
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
                    SourceProp: sourceReadProperties.TryFirst(sp => sp.Name == propToUpdate.Name),
                    PropToUpdate: propToUpdate
                    )
                )
                .Where(x => x.SourceProp.HasValue && x.SourceProp.Value.CanWrite)
                .Select(x => (SourceProp: x.SourceProp.Value, PropToUpdate: x.PropToUpdate));

            foreach (var (sourceProp, propToUpdate) in tuplePropertiesToUpdate)
            {
                CopyPropertyValue(propertiesToUpdate, propToUpdate, newObject, sourceProp);
            }

            return Option<T>.Some(newObject);
        }

        private static List<ConstructorInfo> GetListOfPublicContructorInfo(Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors
                .Where(c => c.IsPublic && !c.IsStatic)
                .ToList();
        }

        private static List<PropertyInfo> GetListOfReadPropertyInfo(Type type)
        {
            return type.GetRuntimeProperties()
                .Where(x => x.CanRead)
                .ToList();
        }

        private static List<PropertyInfo> GetListOfWritePropertyInfo(Type type)
        {
            return type.GetRuntimeProperties()
                .Where(x => x.CanWrite)
                .ToList();
        }

        private static bool AreLinked(MemberInfo memberInfo, ParameterInfo parameterInfo)
        {
            return string.Equals(memberInfo.Name, parameterInfo.Name, StringComparison.CurrentCultureIgnoreCase);
        }

        private static void CopyPropertyValue<T>(T from, PropertyInfo property, T to) where T : class
        {
            property.SetValue(to, property.GetValue(from, null));
        }
        private static void CopyPropertyValue<T1, T2>(T1 from, PropertyInfo fromProperty, T2 to, PropertyInfo toProperty) where T1 : class where T2 : class
        {
            toProperty.SetValue(to, fromProperty.GetValue(from, null));
        }
    }
}
