using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuccincT.Functional
{
    internal sealed class CachedConstructorInfo
    {
        public ConstructorInfo Constructor { get; }
        public List<ParameterInfo> Parameters { get; }

        public CachedConstructorInfo(ConstructorInfo constructorInfo)
            => (Constructor, Parameters) = (constructorInfo, constructorInfo.GetParameters().ToList());
    }
}