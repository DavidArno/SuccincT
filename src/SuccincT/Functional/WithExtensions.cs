using System;
using System.Linq;
using System.Reflection;

namespace SuccincT.Functional
{
    public static class WithExtensions
    {
        public static T Copy<T>(this T @object) where T : class
        {
            var constructors = typeof(T).GetTypeInfo().DeclaredConstructors
                .Where(c => c.IsPublic && !c.IsStatic);

            var constructorToUse = constructors
                .OrderBy(c => c.GetParameters().Length)
                .FirstOrDefault();

            var constructorParameters = constructorToUse.GetParameters();

            var sourceProps = typeof(T).GetRuntimeProperties()
                .Where(x => x.CanRead)
                .ToList();

            var @params = constructorParameters
                .Select(p =>
                {
                    return sourceProps
                        .FirstOrDefault(sp => string.Equals(sp.Name, p.Name, StringComparison.CurrentCultureIgnoreCase))
                        .GetValue(@object, null);
                })
                .ToArray();

            var newObject = Activator.CreateInstance(typeof(T), @params) as T;

            var destProps = typeof(T).GetRuntimeProperties()
                .Where(x => x.CanWrite)
                .ToList();

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

            var newObject = Copy(@object);

            var sourceProps = typeof(T).GetRuntimeProperties()
                .Where(x => x.CanRead)
                .ToList();
            var propsToUpdate = typeof(TProps).GetRuntimeProperties()
                .Where(x => x.CanRead)
                .ToList();

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
    }
}
