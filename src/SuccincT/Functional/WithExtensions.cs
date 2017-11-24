using System.Linq;
using System.Reflection;

namespace SuccincT.Functional
{
    public static class WithExtensions
    {
        public static T Copy<T>(this T @object) where T : new()
        {
            var newObject = new T();

            var sourceProps = typeof(T).GetRuntimeProperties()
                .Where(x => x.CanRead)
                .ToList();

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

        public static T With<T, TProps>(this T @object, TProps propertiesToUpdate) where T : new() where TProps : class
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
