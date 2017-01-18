using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SuccincT.Options;
using SuccincT.Unions;

namespace SuccincT.JSON
{
    public class SuccinctContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<Type, Func<JsonConverter>> _converterProvider =
            new Dictionary<Type, Func<JsonConverter>>
            {
                [typeof(Union<,>)] = () => new UnionOf2Converter(),
                [typeof(Union<,,>)] = () => new UnionOf3Converter(),
                [typeof(Union<,,,>)] = () => new UnionOf4Converter(),
                [typeof(Option<>)] = () => new OptionConverter()
            };

        public static readonly SuccinctContractResolver Instance = new SuccinctContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (!objectType.IsGenericType) return contract;

            var genericType = objectType.GetGenericTypeDefinition();
            if (_converterProvider.ContainsKey(genericType))
            {
                contract.Converter = _converterProvider[genericType]();
            }

            return contract;
        }
    }
}
