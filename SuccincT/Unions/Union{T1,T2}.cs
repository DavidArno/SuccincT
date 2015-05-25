using SuccincT.Exceptions;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions
{
    public class Union<T1, T2>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly Variant _case;

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

        internal Union(T1 case1Value, T2 case2Value, Variant caseToUse)
        {
            if (caseToUse == Variant.Case1)
            {
                _value1 = case1Value;
                _case = Variant.Case1;
            }
            else
            {
                _value2 = case2Value;
                _case = Variant.Case2;
            }
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

        public UnionPatternMatcher<Union<T1, T2>, T1, T2, TReturn> Match<TReturn>()
        {
            return new UnionPatternMatcher<Union<T1, T2>, T1, T2, TReturn>(this);
        }
    }
}
