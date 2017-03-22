using System;
using System.Reflection;

namespace SuccincT.JSON
{
    internal static class TypeExtensions
    {
        internal static bool IsGenericType(this Type type) => type.GetTypeInfo().IsGenericType;

        internal static MethodInfo GetMethod(this Type type, string name) => type.GetTypeInfo().GetDeclaredMethod(name);

        internal static PropertyInfo GetProperty(this Type type, string name) =>
            type.GetTypeInfo().GetDeclaredProperty(name);
    }
}
