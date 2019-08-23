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

        public TResult Value<TResult>()
            => typeof(TResult) switch
            {
                _ when SameType<TResult, T1>() => ItemAs<TResult>(Case1),
                _ when SameType<TResult, T2>() => ItemAs<TResult>(Case2),
                _ when SameType<TResult, T3>() => ItemAs<TResult>(Case3),
                _ when SameType<TResult, T4>() => ItemAs<TResult>(Case4),
                _ => throw new InvalidCaseOfTypeException(typeof(TResult))
            };

        public bool HasValueOf<T>()
            => Case switch
            {
                Variant.Case1 => SameType<T, T1>(),
                Variant.Case2 => SameType<T, T2>(),
                Variant.Case3 => SameType<T, T3>(),
                Variant.Case4 => SameType<T, T4>(),
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
