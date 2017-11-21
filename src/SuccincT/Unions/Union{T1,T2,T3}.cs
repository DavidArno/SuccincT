using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;
using static SuccincT.Functional.Unit;

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

        public Union(T1 value) : this(unit)
        {
            _value1 = value;
            Case = Variant.Case1;
        }

        public Union(T2 value) : this(unit)
        {
            _value2 = value;
            Case = Variant.Case2;
        }

        public Union(T3 value) : this(unit)
        {
            _value3 = value;
            Case = Variant.Case3;
        }

        // ReSharper disable once UnusedParameter.Local - unit param used to 
        // prevent JSON serializer from using this constructor to create an invalid union.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "_")]
        private Union(Unit _)
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

        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);
        public T3 Case3 => Case == Variant.Case3 ? _value3 : throw new InvalidCaseException(Variant.Case3, Case);
        public TResult Value<TResult>()
        {
            if (typeof(TResult) == typeof(T1))
            {
                return (TResult)(object)Case1;
            }
            if (typeof(TResult) == typeof(T2))
            {
                return (TResult)(object)Case2;
            }
            if (typeof(TResult) == typeof(T3))
            {
                return (TResult)(object)Case3;
            }

            throw new InvalidCaseOfTypeException(typeof(TResult));
        }

        public IUnionFuncPatternMatcher<T1, T2, T3, TResult> Match<TResult>() => 
            new UnionFuncPatternMatcher<T1, T2, T3, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2, T3> Match() => 
            new UnionFuncPatternMatcher<T1, T2, T3, Unit>(this);

        public override bool Equals(object obj) => obj is Union<T1, T2, T3> union && UnionsEqual(union);

        public override int GetHashCode() => _hashCodes[Case]();

        public static bool operator ==(Union<T1, T2, T3> a, Union<T1, T2, T3> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return aObj == null && bObj == null || aObj != null && a.Equals(b);
        }

        public static bool operator !=(Union<T1, T2, T3> p1, Union<T1, T2, T3> p2) => !(p1 == p2);

        private bool UnionsEqual(Union<T1, T2, T3> testObject) => 
            Case == testObject.Case && _unionsMatch[Case](testObject);

        public static implicit operator Union<T1, T2, T3>(T1 value) => new Union<T1, T2, T3>(value);
        public static implicit operator Union<T1, T2, T3>(T2 value) => new Union<T1, T2, T3>(value);
        public static implicit operator Union<T1, T2, T3>(T3 value) => new Union<T1, T2, T3>(value);

        Unit IUnion<T1, T2, T3, Unit>.Case4 => throw new InvalidCaseException(Variant.Case4, Case);
    }
}
