using System;
using System.Collections.Generic;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2, T3, T4>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly T4 _value4;
        private readonly Dictionary<Variant, Func<int>> _hashCodes;
        private readonly Dictionary<Variant, Func<Union<T1, T2, T3, T4>, bool>> _unionsMatch;

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

        private Union()
        {
            _hashCodes = new Dictionary<Variant, Func<int>>
            {
                { Variant.Case1, () => _value1.GetHashCode() },
                { Variant.Case2, () => _value1.GetHashCode() },
                { Variant.Case3, () => _value3.GetHashCode() },
                { Variant.Case4, () => _value4.GetHashCode() }
            };
            _unionsMatch = new Dictionary<Variant, Func<Union<T1, T2, T3, T4>, bool>>
            {
                { Variant.Case1, other => EqualityComparer<T1>.Default.Equals(_value1, other._value1) },
                { Variant.Case2, other => EqualityComparer<T2>.Default.Equals(_value2, other._value2) },
                { Variant.Case3, other => EqualityComparer<T3>.Default.Equals(_value3, other._value3) },
                { Variant.Case4, other => EqualityComparer<T4>.Default.Equals(_value4, other._value4) }
            };
        }

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

        public T3 Case3
        {
            get
            {
                if (Case == Variant.Case3) { return _value3; }
                throw new InvalidCaseException(Variant.Case3, Case);
            }
        }

        public T4 Case4
        {
            get
            {
                if (Case == Variant.Case4) { return _value4; }
                throw new InvalidCaseException(Variant.Case4, Case);
            }
        }

        public UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult> Match<TResult>() => 
            new UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>(this);

        public UnionOfFourPatternMatcher<T1, T2, T3, T4> Match() => 
            new UnionOfFourPatternMatcher<T1, T2, T3, T4>(this);

        public override bool Equals(Object obj)
        {
            var testObject = obj as Union<T1, T2, T3, T4>;
            return testObject != null && UnionsEqual(testObject);
        }

        public override int GetHashCode() => 
            _hashCodes[Case]();

        public static bool operator ==(Union<T1, T2, T3, T4> a, Union<T1, T2, T3, T4> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return (aObj == null && bObj == null) || (aObj != null && a.Equals(b));
        }

        public static bool operator !=(Union<T1, T2, T3, T4> a, Union<T1, T2, T3, T4> b) => !(a == b);

        private bool UnionsEqual(Union<T1, T2, T3, T4> testObject) => 
            Case == testObject.Case && _unionsMatch[Case](testObject);
    }
}