using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuccincT.Functional
{
    internal sealed class CachedTypeInfo
    {
        public List<CachedConstructorInfo> CachedPublicConstructors { get; }
        public List<PropertyInfo> Properties { get; }
        public List<PropertyInfo> ReadOnlyProperties { get; }
        public List<PropertyInfo> WriteOnlyProperties { get; }

        public CachedTypeInfo(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            var cachedConstructors =
                typeInfo.DeclaredConstructors.Select(c => new CachedConstructorInfo(c)).ToList();

            CachedPublicConstructors = cachedConstructors
                                      .Where(cc => cc.Constructor.IsPublic && !cc.Constructor.IsStatic).ToList();

            Properties = type.GetRuntimeProperties().ToList();
            ReadOnlyProperties = Properties.Where(p => p.CanRead && !p.CanWrite).ToList();
            WriteOnlyProperties = Properties.Where(p => !p.CanRead && p.CanWrite).ToList();
        }
    }
}