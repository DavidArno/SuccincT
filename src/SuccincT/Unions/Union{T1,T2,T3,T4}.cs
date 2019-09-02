using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;
using static SuccincT.Functional.Unit;
using static SuccincT.Utilities.NRTSupport;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2, T3, T4> : IUnion<T1, T2, T3, T4>
    {
        private readonly T1 _value1 = default!;
        private readonly T2 _value2 = default!;
        private readonly T3 _value3 = default!;
        private readonly T4 _value4 = default!;

        public Variant Case { get; }

        public Union(T1 value) : this(unit) => (_value1, Case) = (value, Variant.Case1);
        public Union(T2 value) : this(unit) => (_value2, Case) = (value, Variant.Case2);
        public Union(T3 value) : this(unit) => (_value3, Case) = (value, Variant.Case3);
        public Union(T4 value) : this(unit) => (_value4, Case) = (value, Variant.Case4);

        // prevent JSON serializer from using this constructor to create an invalid union.
        private Union(Unit _) { }

        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);
        public T3 Case3 => Case == Variant.Case3 ? _value3 : throw new InvalidCaseException(Variant.Case3, Case);
        public T4 Case4 => Case == Variant.Case4 ? _value4 : throw new InvalidCaseException(Variant.Case4, Case);


        public bool HasCase(Variant variant) => variant == Case;

        public T CaseOf<T>()
            => typeof(T) switch
            {
                var t when t == typeof(T1) => Case1.As<T>(),
                var t when t == typeof(T2) => Case2.As<T>(),
                var t when t == typeof(T3) => Case3.As<T>(),
                var t when t == typeof(T4) => Case4.As<T>(),
                _ => throw new InvalidCaseOfTypeException(typeof(T))
            };

        public bool TryCaseOf<T>(out T value)
        {
            var (result, valueTemp) = typeof(T) switch
            {
                var t when t == typeof(T1) && Case == Variant.Case1 => (true, Case1.As<T>()),
                var t when t == typeof(T2) && Case == Variant.Case2 => (true, Case2.As<T>()),
                var t when t == typeof(T3) && Case == Variant.Case3 => (true, Case3.As<T>()),
                var t when t == typeof(T4) && Case == Variant.Case4 => (true, Case4.As<T>()),
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
                Variant.Case4 => TypesAreSame<T, T4>(),
                _ => false,
            };

        public IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult> Match<TResult>() 
            => new UnionPatternMatcher<T1, T2, T3, T4, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2, T3, T4> Match()
            => new UnionPatternMatcher<T1, T2, T3, T4, Unit>(this);

        public override bool Equals(object obj) => obj is Union < T1, T2, T3, T4> union && UnionsEqual(union);

        public override int GetHashCode() =>
            Case switch
            {
                Variant.Case1 => GetItemHashCode(_value1),
                Variant.Case2 => GetItemHashCode(_value2),
                Variant.Case3 => GetItemHashCode(_value3),
                Variant.Case4 => GetItemHashCode(_value4),
                _ => 0
            };

        public static bool operator ==(Union<T1, T2, T3, T4> a, Union<T1, T2, T3, T4> b)
            => a is {} aObj && aObj.Equals(b);

        public static bool operator !=(Union<T1, T2, T3, T4> a, Union<T1, T2, T3, T4> b) => !(a == b);

        public static implicit operator Union<T1, T2, T3, T4>(T1 value) => new Union<T1, T2, T3, T4>(value);
        public static implicit operator Union<T1, T2, T3, T4>(T2 value) => new Union<T1, T2, T3, T4>(value);
        public static implicit operator Union<T1, T2, T3, T4>(T3 value) => new Union<T1, T2, T3, T4>(value);
        public static implicit operator Union<T1, T2, T3, T4>(T4 value) => new Union<T1, T2, T3, T4>(value);

        private bool UnionsEqual(Union<T1, T2, T3, T4> testObject) => Case == testObject.Case && ValuesEqual(testObject);

        private bool ValuesEqual(Union<T1, T2, T3, T4> testObject) =>
            Case switch
            {
                Variant.Case1 => ComparePossibleNullValues(_value1, testObject._value1),
                Variant.Case2 => ComparePossibleNullValues(_value2, testObject._value2),
                Variant.Case3 => ComparePossibleNullValues(_value3, testObject._value3),
                Variant.Case4 => ComparePossibleNullValues(_value4, testObject._value4),
                _ => false
            };
    }
}
