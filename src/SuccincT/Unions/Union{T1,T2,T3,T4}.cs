using SuccincT.Functional;
using SuccincT.Options;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public struct Union<T1, T2, T3, T4>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly T4 _value4;

        public Variant Case { get; }

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

        public Union(T3 value) : this()
        {
            _value3 = value;
            Case = Variant.Case3;
        }

        public Union(T4 value) : this()
        {
            _value4 = value;
            Case = Variant.Case4;
        }

        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);
        public T3 Case3 => Case == Variant.Case3 ? _value3 : throw new InvalidCaseException(Variant.Case3, Case);
        public T4 Case4 => Case == Variant.Case4 ? _value4 : throw new InvalidCaseException(Variant.Case4, Case);

        public TResult Value<TResult>()
        {
            switch (typeof(TResult))
            {
                case var t when t == typeof(T1) && Case1 is TResult value: return value;
                case var t when t == typeof(T2) && Case2 is TResult value: return value;
                case var t when t == typeof(T3) && Case3 is TResult value: return value;
                case var t when t == typeof(T4) && Case4 is TResult value: return value;
                default: throw new InvalidCaseOfTypeException(typeof(TResult));
            }
        }

        public Option<TResult> TryGetValue<TResult>()
        {
            switch (typeof(TResult))
            {
                case var t when t == typeof(T1) && Case1 is TResult value: return value;
                case var t when t == typeof(T2) && Case2 is TResult value: return value;
                case var t when t == typeof(T3) && Case3 is TResult value: return value;
                case var t when t == typeof(T4) && Case4 is TResult value: return value;
                default: return Option<TResult>.None();
            }
        }

        public IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult> Match<TResult>()
            => new UnionPatternMatcher<T1, T2, T3, T4, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2, T3, T4> Match()
            => new UnionPatternMatcher<T1, T2, T3, T4, Unit>(this);


        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Union<T1, T2, T3, T4> union: return UnionsEqual(union);
                case null:
                    switch (Case)
                    {
                        case Variant.Case1: return _value1 == null;
                        case Variant.Case2: return _value2 == null;
                        case Variant.Case3: return _value3 == null;
                        default: return _value4 == null;
                    }
            }

            return false;
        }

        public override int GetHashCode() => GetValueHashCode();

        public static bool operator ==(Union<T1, T2, T3, T4> a, Union<T1, T2, T3, T4> b) => a.Equals(b);

        public static bool operator !=(Union<T1, T2, T3, T4> a, Union<T1, T2, T3, T4> b) => !(a == b);

        public static implicit operator Union<T1, T2, T3, T4>(T1 value) => new Union<T1, T2, T3, T4>(value);
        public static implicit operator Union<T1, T2, T3, T4>(T2 value) => new Union<T1, T2, T3, T4>(value);
        public static implicit operator Union<T1, T2, T3, T4>(T3 value) => new Union<T1, T2, T3, T4>(value);
        public static implicit operator Union<T1, T2, T3, T4>(T4 value) => new Union<T1, T2, T3, T4>(value);

        private bool UnionsEqual(Union<T1, T2, T3, T4> testObject) 
            => Case == testObject.Case && ValuesEqual(testObject);

        private bool ValuesEqual(Union<T1, T2, T3, T4> other)
        {
            switch (Case)
            {
                case Variant.Case1:
                    return _value1 == null && other._value1 == null || _value1 != null && _value1.Equals(other._value1);
                case Variant.Case2:
                    return _value2 == null && other._value2 == null || _value2 != null && _value2.Equals(other._value2);
                case Variant.Case3:
                    return _value3 == null && other._value3 == null || _value3 != null && _value3.Equals(other._value3);
                default:
                    return _value4 == null && other._value4 == null || _value4 != null && _value4.Equals(other._value4);
            }
        }

        private int GetValueHashCode()
        {
            switch (Case)
            {
                case Variant.Case1: return _value1.GetHashCode();
                case Variant.Case2: return _value2.GetHashCode();
                case Variant.Case3: return _value3.GetHashCode();
                default: return _value4.GetHashCode();
            }
        }
    }
}
