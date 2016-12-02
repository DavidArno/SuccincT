using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public sealed class Union<T1, T2, T3> : IUnion<T1, T2, T3, Unit>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly Dictionary<Variant, Func<int>> _hashCodes;
        private readonly Dictionary<Variant, Func<Union<T1, T2, T3>, bool>> _unionsMatch;


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

        private Union()
        {
            _hashCodes = new Dictionary<Variant, Func<int>>
            {
                { Variant.Case1, () => _value1.GetHashCode() },
                { Variant.Case2, () => _value2.GetHashCode() },
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
                if (Case != Variant.Case1) { throw new InvalidCaseException(Variant.Case1, Case); }
                return _value1;
            }
        }

        public T2 Case2
        {
            get
            {
                if (Case != Variant.Case2) { throw new InvalidCaseException(Variant.Case2, Case); }
                return _value2;
            }
        }

        public T3 Case3
        {
            get
            {
                if (Case != Variant.Case3) { throw new InvalidCaseException(Variant.Case3, Case); }
                return _value3;
            }
        }

        public IUnionFuncPatternMatcher<T1, T2, T3, TResult> Match<TResult>() => 
            new UnionFuncPatternMatcher<T1, T2, T3, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2, T3> Match() => 
            new UnionFuncPatternMatcher<T1, T2, T3, Unit>(this);

        public override bool Equals(object obj)
        {
            var testObject = obj as Union<T1, T2, T3>;
            return testObject != null && UnionsEqual(testObject);
        }

        public override int GetHashCode() => _hashCodes[Case]();

        public static bool operator ==(Union<T1, T2, T3> a, Union<T1, T2, T3> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return (aObj == null && bObj == null) || (aObj != null && a.Equals(b));
        }

        public static bool operator !=(Union<T1, T2, T3> p1, Union<T1, T2, T3> p2) => !(p1 == p2);

        private bool UnionsEqual(Union<T1, T2, T3> testObject) => 
            Case == testObject.Case && _unionsMatch[Case](testObject);
        Unit IUnion<T1, T2, T3, Unit>.Case4 { get { throw new InvalidCaseException(Variant.Case4, Case); } }
    }
}
