using System;
using System.Collections.Generic;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2, T3>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly Variant _case;
        private readonly Dictionary<Variant, Func<int>> _hashCodes;
        private readonly Dictionary<Variant, Func<Union<T1, T2, T3>, bool>> _unionsMatch;

        public static UnionCreator<T1, T2, T3> Creator()
        {
            return new UnionCreator<T1, T2, T3>();
        }

        public Variant Case
        {
            get { return _case; }
        }

        public Union(T1 value) : this()
        {
            _value1 = value;
            _case = Variant.Case1;
        }

        public Union(T2 value) : this()
        {
            _value2 = value;
            _case = Variant.Case2;
        }

        public Union(T3 value) : this()
        {
            _value3 = value;
            _case = Variant.Case3;
        }

        private Union()
        {
            _hashCodes = new Dictionary<Variant, Func<int>>
            {
                { Variant.Case1, () => _value1.GetHashCode() },
                { Variant.Case2, () => _value1.GetHashCode() },
                { Variant.Case3, () => _value3.GetHashCode() }
            };
            _unionsMatch = new Dictionary<Variant, Func<Union<T1, T2, T3>, bool>>
            {
                { Variant.Case1, other => EqualityComparer<T1>.Default.Equals(_value1, other._value1) },
                { Variant.Case2, other => EqualityComparer<T2>.Default.Equals(_value2, other._value2) },
                { Variant.Case3, other => EqualityComparer<T3>.Default.Equals(_value3, other._value3) }
            };
        }

        public T1 Case1
        {
            get
            {
                if (_case != Variant.Case1) { throw new InvalidCaseException(Variant.Case1, _case); }
                return _value1;
            }
        }

        public T2 Case2
        {
            get
            {
                if (_case != Variant.Case2) { throw new InvalidCaseException(Variant.Case2, _case); }
                return _value2;
            }
        }

        public T3 Case3
        {
            get
            {
                if (_case != Variant.Case3) { throw new InvalidCaseException(Variant.Case3, _case); }
                return _value3;
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

        public override bool Equals(Object obj)
        {
            var testObject = obj as Union<T1, T2, T3>;
            return obj is Union<T1, T2, T3> && UnionsEqual(testObject);
        }

        public override int GetHashCode()
        {
            return _hashCodes[Case]();
        }

        public static bool operator ==(Union<T1, T2, T3> a, Union<T1, T2, T3> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return (aObj == null && bObj == null) || (aObj != null && a.Equals(b));
        }

        public static bool operator !=(Union<T1, T2, T3> a, Union<T1, T2, T3> b)
        {
            return !(a == b);
        }

        private bool UnionsEqual(Union<T1, T2, T3> testObject)
        {
            return Case == testObject.Case && _unionsMatch[Case](testObject);
        }
    }
}