// Contains methods required to allow the dotnetcore version to compile against the portable class library files
using System;

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
    }
}
