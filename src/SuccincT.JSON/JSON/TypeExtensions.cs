using System;
using System.Reflection;

namespace SuccincT.JSON
{
    internal static class TypeExtensions
    {
        internal static bool IsGenericType(this Type type) => type.GetTypeInfo().IsGenericType;
    }
}
