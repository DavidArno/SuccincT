using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;
using System;
using static SuccincT.Utilities.NRTSupport;

namespace SuccincT.Unions
{
    public readonly struct Union<T1, T2, T3> : IUnion<T1, T2, T3, Unit>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;

        public Variant Case { get; }

        public Union(T1 value) => (_value1, _value2, _value3, Case) = (value, default!, default!, Variant.Case1);
        public Union(T2 value) => (_value1, _value2, _value3, Case) = (default!, value, default!, Variant.Case2);
        public Union(T3 value) => (_value1, _value2, _value3, Case) = (default!, default!, value, Variant.Case3);


        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);
        public T3 Case3 => Case == Variant.Case3 ? _value3 : throw new InvalidCaseException(Variant.Case3, Case);

        public bool HasCase(Variant variant) => variant == Case;

        public T CaseOf<T>()
            => typeof(T) switch
            {
                var t when t == typeof(T1) => Case1.As<T>(),
                var t when t == typeof(T2) => Case2.As<T>(),
                var t when t == typeof(T3) => Case3.As<T>(),
                _ => throw new InvalidCaseOfTypeException(typeof(T))
            };

        public bool TryCaseOf<T>(out T value)
        {
            var (result, valueTemp) = typeof(T) switch
            {
                var t when t == typeof(T1) && Case == Variant.Case1 => (true, Case1.As<T>()),
                var t when t == typeof(T2) && Case == Variant.Case2 => (true, Case2.As<T>()),
                var t when t == typeof(T3) && Case == Variant.Case3 => (true, Case3.As<T>()),
                _ => (false, default)
            };

            value = valueTemp;
            return result;
        }

        public bool HasCaseOf<T>()
            => Case switch
            {
                Variant.Case1 => TypesAreSame<T, T1>(),
                Variant.Case2 => TypesAreSame<T, T2>(),
                Variant.Case3 => TypesAreSame<T, T3>(),
                _ => false,
            };

        public IUnionFuncPatternMatcher<T1, T2, T3, TResult> Match<TResult>()
            => new UnionFuncPatternMatcher<T1, T2, T3, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2, T3> Match() 
            => new UnionFuncPatternMatcher<T1, T2, T3, Unit>(this);

        public void Deconstruct(out Variant variant, out T1 case1, out T2 case2, out T3 case3)
            => (variant, case1, case2, case3) = (Case, _value1, _value2, _value3);

        public override bool Equals(object obj) => obj is Union<T1, T2, T3> union && UnionsEqual(union);

        public override int GetHashCode() 
            => Case switch
            {
                Variant.Case1 => GetItemHashCode(_value1),
                Variant.Case2 => GetItemHashCode(_value2),
                Variant.Case3 => GetItemHashCode(_value3),
                _ => 0
            };

        public static bool operator ==(Union<T1, T2, T3> a, Union<T1, T2, T3> b)
            => a is {} aObj && aObj.Equals(b);

        public static bool operator !=(Union<T1, T2, T3> a, Union<T1, T2, T3> b) => !(a == b);

        public static implicit operator Union<T1, T2, T3>(T1 value)
            => value is { }
                ? new Union<T1, T2, T3>(value)
                : throw new InvalidCastException("Cannot cast null to a Union<T1,T2, T3>.");

        public static implicit operator Union<T1, T2, T3>(T2 value)
            => value is { }
                ? new Union<T1, T2, T3>(value)
                : throw new InvalidCastException("Cannot cast null to a Union<T1,T2, T3>.");

        public static implicit operator Union<T1, T2, T3>(T3 value)
            => value is { }
                ? new Union<T1, T2, T3>(value)
                : throw new InvalidCastException("Cannot cast null to a Union<T1,T2, T3>.");

        private bool UnionsEqual(Union<T1, T2, T3> testObject) => Case == testObject.Case && ValuesEqual(testObject);

        private bool ValuesEqual(Union<T1, T2, T3> testObject) =>
            Case switch
            {
                Variant.Case1 => ComparePossibleNullValues(_value1, testObject._value1),
                Variant.Case2 => ComparePossibleNullValues(_value2, testObject._value2),
                Variant.Case3 => ComparePossibleNullValues(_value3, testObject._value3),
                _ => false
            };

        Unit IUnion<T1, T2, T3, Unit>.Case4 => throw new InvalidCaseException(Variant.Case4, Case);
    }
}
