using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public class Union<T1, T2>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;

        public Union(T1 value)
        {
            _value1 = value;
            Case = Variant.Case1;
        }

        public Union(T2 value)
        {
            _value2 = value;
            Case = Variant.Case2;
        }

        internal Union(T1 case1Value, T2 case2Value, Variant caseToUse)
        {
            if (caseToUse == Variant.Case1)
            {
                _value1 = case1Value;
                Case = Variant.Case1;
            }
            else
            {
                _value2 = case2Value;
                Case = Variant.Case2;
            }
        }

        public Variant Case { get; }

        public T1 Case1
        {
            get
            {
                if (Case == Variant.Case1) { return _value1; }
                throw new InvalidCaseException(Variant.Case1, Case);
            }
        }

        public T2 Case2
        {
            get
            {
                if (Case == Variant.Case2) { return _value2; }
                throw new InvalidCaseException(Variant.Case2, Case);
            }
        }

        public UnionOfTwoPatternMatcher<Union<T1, T2>, T1, T2, TReturn> Match<TReturn>()
        {
            return new UnionOfTwoPatternMatcher<Union<T1, T2>, T1, T2, TReturn>(this);
        }

        public UnionOfTwoPatternMatcher<Union<T1, T2>, T1, T2> Match()
        {
            return new UnionOfTwoPatternMatcher<Union<T1, T2>, T1, T2>(this);
        }
    }
}