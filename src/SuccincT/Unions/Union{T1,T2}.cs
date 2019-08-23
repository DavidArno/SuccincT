using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;
using static SuccincT.Functional.Unit;
using static SuccincT.Utilities.NRTSupport;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2> : IUnion<T1, T2, Unit, Unit>
    {
        private readonly T1 _value1 = default!;
        private readonly T2 _value2 = default!;

        public Variant Case { get; }

        public Union(T1 value) : this(unit) => (_value1, Case) = (value, Variant.Case1);
        public Union(T2 value) : this(unit) => (_value2, Case) = (value, Variant.Case2);

        // prevent JSON serializer from using this constructor to create an invalid union.
        private Union(Unit _) { }

        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);

        public TResult Value<TResult>()
            => typeof(TResult) switch
            {
                _ when SameType<TResult, T1>() => ItemAs<TResult>(Case1),
                _ when SameType<TResult, T2>() => ItemAs<TResult>(Case2),
                _ => throw new InvalidCaseOfTypeException(typeof(TResult))
            };

        public bool HasValueOf<T>()
            => Case switch
            {
                Variant.Case1 => SameType<T, T1>(),
                Variant.Case2 => SameType<T, T2>(),
                _ => false,
            };

        public IUnionFuncPatternMatcher<T1, T2, TResult> Match<TResult>()
            => new UnionPatternMatcher<T1, T2, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2> Match() => new UnionPatternMatcher<T1, T2, Unit>(this);

        public override bool Equals(object obj) => obj is Union<T1, T2> union && UnionsEqual(union);

        public override int GetHashCode() =>
            Case switch
            {
                Variant.Case1 => GetItemHashCode(_value1),
                Variant.Case2 => GetItemHashCode(_value2),
                _ => 0
            };

        public static bool operator ==(Union<T1, T2> a, Union<T1, T2> b)
            => a is { } aObj && aObj.Equals(b);

        public static bool operator !=(Union<T1, T2> a, Union<T1, T2> b) => !(a == b);

        public static implicit operator Union<T1, T2>(T1 value) => new Union<T1, T2>(value);
        public static implicit operator Union<T1, T2>(T2 value) => new Union<T1, T2>(value);

        private bool UnionsEqual(Union<T1, T2> testObject) => Case == testObject.Case && ValuesEqual(testObject);

        private bool ValuesEqual(Union<T1, T2> testObject) =>
            Case switch
            {
                Variant.Case1 => ComparePossibleNullValues(_value1, testObject._value1),
                Variant.Case2 => ComparePossibleNullValues(_value2, testObject._value2),
                _ => false
            };
        Unit IUnion<T1, T2, Unit, Unit>.Case3 => throw new InvalidCaseException(Variant.Case3, Case);
        Unit IUnion<T1, T2, Unit, Unit>.Case4 => throw new InvalidCaseException(Variant.Case4, Case);
    }
}
