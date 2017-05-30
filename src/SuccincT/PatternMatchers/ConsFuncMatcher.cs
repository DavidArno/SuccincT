using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    internal class ConsFuncMatcher<T, TResult> : IConsFuncMatcher<T, TResult>,
                                                 IConsFuncNoneHandler<T, TResult>,
                                                 IConsFuncSingleHandler<T, TResult>,
                                                 IConsFuncSingleWhereHandler<T, TResult>
    {
        private readonly IEnumerable<T> _collection;
        private TResult _emptyValue;
        private Func<T, bool> _whereFunc;
        private List<(Func<T, bool> testFunc, TResult value)> _singleTests;

        internal ConsFuncMatcher(IEnumerable<T> collection)
        {
            _collection = collection;
            _singleTests = new List<(Func<T, bool> testFunc, TResult value)>();
        }

        IConsFuncNoneHandler<T, TResult> IConsFuncMatcher<T, TResult>.Empty() => this;
        IConsFuncSingleHandler<T, TResult> IConsFuncMatcher<T, TResult>.Single() => this;

        TResult IConsFuncMatcher<T, TResult>.Result() =>  _collection.Count() == 0 ? _emptyValue : SingleMatch();

        IConsFuncMatcher<T, TResult> IConsFuncNoneHandler<T, TResult>.Do(TResult value)
        {
            _emptyValue = value;
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleHandler<T, TResult>.Do(TResult value)
        {
            _singleTests.Add((x => true, value));
            return this;
        }

        IConsFuncSingleWhereHandler<T, TResult> IConsFuncSingleHandler<T, TResult>.Where(Func<T, bool> testFunc)
        {
            _whereFunc = testFunc;
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleWhereHandler<T, TResult>.Do(TResult value)
        {
            _singleTests.Add((_whereFunc, value));
            return this;
        }
        private TResult SingleMatch()
        {
            foreach(var (testFunc, value) in _singleTests)
            {
                if (testFunc(_collection.First()))
                {
                    return value;
                }
            }
            return default(TResult);
        }
    }
}