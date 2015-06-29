using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2, T3, T4>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly T4 _value4;
        private readonly Variant _case;

        public static UnionCreator<T1, T2, T3, T4> Creator()
        {
            return new UnionCreator<T1, T2, T3, T4>();
        }

        public Variant Case { get { return _case; } }

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

        public Union(T4 value)
        {
            _value4 = value;
            _case = Variant.Case4;
        }

        public T1 Case1
        {
            get
            {
                if (_case == Variant.Case1) { return _value1; }
                throw new InvalidCaseException(Variant.Case1, _case);
            }
        }

        public T2 Case2
        {
            get
            {
                if (_case == Variant.Case2) { return _value2; }
                throw new InvalidCaseException(Variant.Case2, _case);
            }
        }

        public T3 Case3
        {
            get
            {
                if (_case == Variant.Case3) { return _value3; }
                throw new InvalidCaseException(Variant.Case3, _case);
            }
        }

        public T4 Case4
        {
            get
            {
                if (_case == Variant.Case4) { return _value4; }
                throw new InvalidCaseException(Variant.Case4, _case);
            }
        }

        public UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult> Match<TResult>()
        {
            return new UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>(this);
        }

        public UnionOfFourPatternMatcher<T1, T2, T3, T4> Match()
        {
            return new UnionOfFourPatternMatcher<T1, T2, T3, T4>(this);
        }
    }
}