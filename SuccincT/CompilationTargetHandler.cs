// Contains methods required to allow the dotnetcore version to compile against the portable class library files
using System;
using System.Reflection;

namespace SuccincT
{
    public static class CompilationTargetHandler
    {
        public static bool IsEnum(this Type type) =>
#if CORE
            type.GetTypeInfo().IsEnum;
#else
            type.IsEnum;
#endif
        
        public static MethodInfo GetMethodInfo(this Type type, string method, Type[] types) =>
#if CORE
            type.GetTypeInfo().GetMethod(method, types);
#else
            type.GetMethod(method, types);
#endif
    }
}
