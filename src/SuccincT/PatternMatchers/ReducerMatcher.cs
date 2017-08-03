using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Functional;

namespace SuccincT.PatternMatchers
{
    internal class ReducerMatcher<T, TResult> : IReducerMatcher<T, TResult>,
                                                IReducerNoneHandler<T, TResult>, 
                                                IReducerSingleHandler<T, TResult>
    {
        private readonly IEnumerable<T> _collection;
        private TResult _noneValue;
        private List<Func<T, TResult>> _singleTestDos;

        public ReducerMatcher(IEnumerable<T> collection)
        {
            _collection = collection;
            _singleTestDos = new List<Func<T, TResult>>();
        }

        IReducerNoneHandler<T, TResult> IReducerMatcher<T, TResult>.Empty() => this;

        IReducerSingleHandler<T, TResult> IReducerMatcher<T, TResult>.Single() => this;

        IReducerMatcher<T, TResult> IReducerNoneHandler<T, TResult>.Do(TResult doValue)
        {
            _noneValue = doValue;
            return this;
        }

        IReducerMatcher<T, TResult> IReducerSingleHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTestDos.Add(doFunc);
            return this;
        }

        IReducerMatcher<T, TResult> IReducerSingleHandler<T, TResult>.Do(TResult doValue)
        {
            _singleTestDos.Add(_ => doValue);
            return this;
        }

        IEnumerable<TResult> IReducerMatcher<T, TResult>.Result()
        {
            if (!_collection.Any())
            {
                return new ConsEnumerable<TResult>(_noneValue);
            }
            return new ConsEnumerable<TResult>(_singleTestDos[0](_collection.First()));
        }
    }
}