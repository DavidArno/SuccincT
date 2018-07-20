using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public readonly struct Union<T1, T2>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;

        public Union(T1 value) : this()
        {
            _value1 = value;
            Case = Variant.Case1;
        }

        public Union(T2 value) : this()
        {
            _value2 = value;
            Case = Variant.Case2;
        }

        public Variant Case { get; }

        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);

        public TResult Value<TResult>()
        {
            switch (typeof(TResult))
            {
                case var t when t == typeof(T1) && Case1 is TResult value: return value;
                case var t when t == typeof(T2) && Case2 is TResult value: return value;
                default: throw new InvalidCaseOfTypeException(typeof(TResult));
            }
        }

        public IUnionFuncPatternMatcher<T1, T2, TResult> Match<TResult>()
            => new UnionPatternMatcher<T1, T2, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2> Match() => new UnionPatternMatcher<T1, T2, Unit>(this);

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Union<T1, T2> union: return UnionsEqual(union);
                case null: return Case == Variant.Case1 && _value1 == null || Case == Variant.Case2 && _value2 == null;
            }

            return false;
        }

        public override int GetHashCode() => GetValueHashCode();

        public static bool operator ==(Union<T1, T2> p1, Union<T1, T2> p2) => p1.Equals(p2);

        public static bool operator !=(Union<T1, T2> a, Union<T1, T2> b) => !(a == b);

        private bool UnionsEqual(Union<T1, T2> testObject) => Case == testObject.Case && ValuesEqual(testObject);

        private int GetValueHashCode() => Case == Variant.Case1 ? _value1.GetHashCode() : _value2.GetHashCode();

        private bool ValuesEqual(Union<T1, T2> other)
        {
            return Case == Variant.Case1
                ? _value1 == null && other._value1 == null || _value1 != null && _value1.Equals(other._value1)
                : _value2 == null && other._value2 == null || _value2 != null && _value2.Equals(other._value2);
        }

        public static implicit operator Union<T1, T2>(T1 value) => new Union<T1, T2>(value);
        public static implicit operator Union<T1, T2>(T2 value) => new Union<T1, T2>(value);
    }
}