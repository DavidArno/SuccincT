using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2, T3>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly Variant _case;

        public static UnionCreator<T1, T2, T3> Creator()
        {
            return new UnionCreator<T1, T2, T3>();
        }

        public Variant Case
        {
            get { return _case; }
        }

        public Union(T1 value)
        {
            _value1 = value;
            _case = Variant.Case1;
        }

        public Union(T2 value)
        {
            _value2 = value;
            _case = Variant.Case2;
        }

        public Union(T3 value)
        {
            _value3 = value;
            _case = Variant.Case3;
        }

        public T1 Case1
        {
            get
            {
                if (_case == Variant.Case1)
                {
                    return _value1;
                }
                throw new InvalidCaseException(Variant.Case1, _case);
            }
        }

        public T2 Case2
        {
            get
            {
                if (_case == Variant.Case2)
                {
                    return _value2;
                }
                throw new InvalidCaseException(Variant.Case2, _case);
            }
        }

        public T3 Case3
        {
            get
            {
                if (_case == Variant.Case3)
                {
                    return _value3;
                }
                throw new InvalidCaseException(Variant.Case3, _case);
            }
        }

        public UnionOfThreePatternMatcher<T1, T2, T3, TResult> Match<TResult>()
        {
            return new UnionOfThreePatternMatcher<T1, T2, T3, TResult>(this);
        }

        public UnionOfThreePatternMatcher<T1, T2, T3> Match()
        {
            return new UnionOfThreePatternMatcher<T1, T2, T3>(this);
        }
    }
}