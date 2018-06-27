using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SuccincT.Functional;
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
                [typeof(Option<>)] = () => new OptionConverter(),
                [typeof(ValueOrError)] = () => new ValueOrErrorConverter(),
                [typeof(None)] = () => new NoneAndUnitConverter(),
                [typeof(Unit)] = () => new NoneAndUnitConverter(),
                [typeof(Either<,>)] = () => new EitherConverter(),
                [typeof(Success<>)] = () => new SuccessConverter()
            };

        public static readonly SuccinctContractResolver Instance = new SuccinctContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (_converterProvider.ContainsKey(objectType))
            {
                contract.Converter = _converterProvider[objectType]();
            }
            else if (objectType.IsGenericType())
            {
                var genericType = objectType.GetGenericTypeDefinition();
                if (_converterProvider.ContainsKey(genericType))
                {
                    contract.Converter = _converterProvider[genericType]();
                }
            }

            return contract;
        }
    }
}
