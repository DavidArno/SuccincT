using Newtonsoft.Json.Serialization;
using System;
using SuccincT.Unions;

namespace SuccincT.JSON
{
    public class SuccinctContractResolver : DefaultContractResolver
    {
        public static readonly SuccinctContractResolver Instance = new SuccinctContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (objectType.IsGenericType)
            {
                if (objectType.GetGenericTypeDefinition() == typeof(Union<,>))
                {
                    contract.Converter = new UnionOf2Converter();
                }

                if (objectType.GetGenericTypeDefinition() == typeof(Union<,,>))
                {
                    contract.Converter = new UnionOf3Converter();
                }

                if (objectType.GetGenericTypeDefinition() == typeof(Union<,,,>))
                {
                    contract.Converter = new UnionOf4Converter();
                }
            }
            return contract;
        }
    }
}
