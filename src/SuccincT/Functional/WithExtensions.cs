using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuccincT.Functional
{
    public static class WithExtensions
    {
        public static T Copy<T>(this T @object) where T : class
        {
            var constructors = GetListOfPublicContructorInfo(typeof(T));

            var constructorToUse = constructors
                .OrderBy(c => c.GetParameters().Length)
                .FirstOrDefault();

            var constructorParameters = constructorToUse.GetParameters();

            var sourceProps = GetListOfReadPropertyInfo(typeof(T));

            var @params = constructorParameters
                .Select(p =>
                {
                    return sourceProps
                        .FirstOrDefault(sp => string.Equals(sp.Name, p.Name, StringComparison.CurrentCultureIgnoreCase))
                        .GetValue(@object, null);
                })
                .ToArray();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            var destProps = GetListOfWritePropertyInfo(typeof(T));

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { 
                        p.SetValue(newObject, sourceProp.GetValue(@object, null), null);
                    }
                }
            }

            return newObject;
        }

        public static T With<T, TProps>(this T @object, TProps propertiesToUpdate) where T : class where TProps : class
        {
            if (propertiesToUpdate == null)
                return @object;

            var sourceProps = GetListOfReadPropertyInfo(typeof(T));
            var propsToUpdate = GetListOfReadPropertyInfo(typeof(TProps));

            var constructors = GetListOfPublicContructorInfo(typeof(T));

            var constructorToUse = constructors
                                       .Where(c =>
                                       {
                                           var parameters = c.GetParameters();
                                           return parameters.All(p => propsToUpdate.Any(ptu =>
                                               string.Equals(ptu.Name, p.Name, StringComparison.CurrentCultureIgnoreCase)));
                                       })
                                       .OrderBy(c => c.GetParameters().Length)
                                       .FirstOrDefault()
                                   ?? constructors.FirstOrDefault();

            var constructorParameters = constructorToUse.GetParameters();

            var @params = constructorParameters
                .Select(p =>
                {
                    if (propsToUpdate.Any(ptu => string.Equals(ptu.Name, p.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        return propsToUpdate
                            .FirstOrDefault(ptu => string.Equals(ptu.Name, p.Name, StringComparison.CurrentCultureIgnoreCase))
                            .GetValue(propertiesToUpdate, null);
                    }

                    return sourceProps
                        .FirstOrDefault(sp => string.Equals(sp.Name, p.Name, StringComparison.CurrentCultureIgnoreCase))
                        .GetValue(@object, null);
                })
                .ToArray();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            var destProps = GetListOfWritePropertyInfo(typeof(T));

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    {
                        p.SetValue(newObject, sourceProp.GetValue(@object, null), null);
                    }
                }
            }

            foreach (var propToUpdate in propsToUpdate)
            {
                var updatingProp = sourceProps.FirstOrDefault(sp => sp.Name == propToUpdate.Name);

                if (updatingProp != null && updatingProp.CanWrite)
                {
                    updatingProp.SetValue(newObject, propToUpdate.GetValue(propertiesToUpdate, null), null);
                }
            }

            return newObject;
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

        private static List<ConstructorInfo> GetListOfPublicContructorInfo(Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors
                .Where(c => c.IsPublic && !c.IsStatic)
                .ToList();
        }
    }
}
