using System;
using System.Collections.Generic;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly Variant _case;
        private readonly Dictionary<Variant, Func<int>> _hashCodes;
        private readonly Dictionary<Variant, Func<Union<T1, T2>, bool>> _unionsMatch;

        public static UnionCreator<T1, T2> Creator()
        {
            return new UnionCreator<T1, T2>();
        }

        private Union()
        {
            _hashCodes = new Dictionary<Variant, Func<int>>
            {
                { Variant.Case1, () => _value1.GetHashCode() },
                { Variant.Case2, () => _value2.GetHashCode() }
            };
            _unionsMatch = new Dictionary<Variant, Func<Union<T1, T2>, bool>>
            {
                { Variant.Case1, other => EqualityComparer<T1>.Default.Equals(_value1, other._value1) },
                { Variant.Case2, other => EqualityComparer<T2>.Default.Equals(_value2, other._value2) }
            };
        }

        public Variant Case { get { return _case; } }

        public Union(T1 value)
            : this()
        {
            _value1 = value;
            _case = Variant.Case1;
        }

        public Union(T2 value)
            : this()
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

        public UnionOfTwoPatternMatcher<T1, T2, TReturn> Match<TReturn>()
        {
            return new UnionOfTwoPatternMatcher<T1, T2, TReturn>(this);
        }

        public UnionOfTwoPatternMatcher<T1, T2> Match()
        {
            return new UnionOfTwoPatternMatcher<T1, T2>(this);
        }

        public override bool Equals(Object obj)
        {
            var testObject = obj as Union<T1, T2>;
            return obj is Union<T1, T2> && UnionsEqual(testObject);
        }

        public override int GetHashCode()
        {
            return _hashCodes[Case]();
        }

        public static bool operator ==(Union<T1, T2> a, Union<T1, T2> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return (aObj == null && bObj == null) || (aObj != null && a.Equals(b));
        }

        public static bool operator !=(Union<T1, T2> a, Union<T1, T2> b)
        {
            return !(a == b);
        }

        private bool UnionsEqual(Union<T1, T2> testObject)
        {
            return _unionsMatch[Case](testObject);
        }
    }
}