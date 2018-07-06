using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.Unions.PatternMatchers;
using static SuccincT.Functional.Unit;

namespace SuccincT.Unions
{
    public readonly struct Union<T1, T2, T3>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;

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
        private Union(Unit _) : this() { }

        public T1 Case1 => Case == Variant.Case1 ? _value1 : throw new InvalidCaseException(Variant.Case1, Case);
        public T2 Case2 => Case == Variant.Case2 ? _value2 : throw new InvalidCaseException(Variant.Case2, Case);
        public T3 Case3 => Case == Variant.Case3 ? _value3 : throw new InvalidCaseException(Variant.Case3, Case);

        public TResult Value<TResult>()
        {
            switch (typeof(TResult))
            {
                case var t when t == typeof(T1): return (TResult)(object)Case1;
                case var t when t == typeof(T2): return (TResult)(object)Case2;
                case var t when t == typeof(T3): return (TResult)(object)Case3;
                default: throw new InvalidCaseOfTypeException(typeof(TResult));
            }
        }

        public IUnionFuncPatternMatcher<T1, T2, T3, TResult> Match<TResult>() =>
            new UnionFuncPatternMatcher<T1, T2, T3, TResult>(this);

        public IUnionActionPatternMatcher<T1, T2, T3> Match() =>
            new UnionFuncPatternMatcher<T1, T2, T3, Unit>(this);

        public override bool Equals(object obj)
        {
            if (obj is Union<T1, T2, T3> union)
            {
                return UnionsEqual(union);
            }

            if (obj == null)
            {
                switch (Case)
                {
                    case Variant.Case1: return _value1 == null;
                    case Variant.Case2: return _value2 == null;
                    default: return _value3 == null;
                }
            }
            return false;
        }

        public override int GetHashCode() => GetValueHashCode();

        public static bool operator ==(Union<T1, T2, T3> a, Union<T1, T2, T3> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return aObj == null && bObj == null || aObj != null && a.Equals(b);
        }

        public static bool operator !=(Union<T1, T2, T3> p1, Union<T1, T2, T3> p2) => !(p1 == p2);

        public static implicit operator Union<T1, T2, T3>(T1 value) => new Union<T1, T2, T3>(value);
        public static implicit operator Union<T1, T2, T3>(T2 value) => new Union<T1, T2, T3>(value);
        public static implicit operator Union<T1, T2, T3>(T3 value) => new Union<T1, T2, T3>(value);

        private bool UnionsEqual(Union<T1, T2, T3> testObject) => Case == testObject.Case && ValuesEqual(testObject);

        private bool ValuesEqual(Union<T1, T2, T3> other)
        {
            switch (Case)
            {
                case Variant.Case1: return _value1 == null && other._value1 == null || _value1 != null && _value1.Equals(other._value1);
                case Variant.Case2: return _value2 == null && other._value2 == null || _value2 != null && _value2.Equals(other._value2);
                default: return _value3 == null && other._value3 == null || _value3 != null && _value3.Equals(other._value3);
            }
        }

        private int GetValueHashCode()
        {
            switch (Case)
            {
                case Variant.Case1: return _value1.GetHashCode();
                case Variant.Case2: return _value2.GetHashCode();
                default: return _value3.GetHashCode();
            }
        }
    }
}