using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal sealed class TypeMatcher<T, TResult> : ITypeMatcher<T, TResult>, 
                                                    ITypeMatcherFuncPatternMatcherAfterElse<TResult>
    {
        private readonly T _item;
        private readonly List<TypeMatcherCaseOfData<T, TResult>> _matchList;

        private Func<T, TResult> _elseFunction;

        internal TypeMatcher(T item)
        {
            _elseFunction = null!;

            _item = item;
            _matchList = new List<TypeMatcherCaseOfData<T, TResult>>();
        }

        public ITypeMatcherCaseHandler<ITypeMatcher<T, TResult>, TCaseType, TResult> CaseOf<TCaseType>() 
            where TCaseType : T => 
            new TypeMatcherCaseHandler<T, TCaseType, TResult>(this, data => _matchList.Add(data));

        public ITypeMatcherFuncPatternMatcherAfterElse<TResult> Else(Func<T, TResult> elseFunction)
        {
            _elseFunction = elseFunction;
            return this;
        }

        public ITypeMatcherFuncPatternMatcherAfterElse<TResult> Else(TResult value)
        {
            _elseFunction = _ => value;
            return this;
        }

        public TResult Result()
        {
            foreach (var match in _matchList)
            {
                if (_item!.GetType() == match.CaseType && match.WhereExpression(_item))
                {
                    return match.DoFunc(_item);
                }
            }

            if (_elseFunction != null)
            {
                return _elseFunction(_item);
            }

            throw new NoMatchException($"No match exists for value of {_item}");
        }

    }
}
