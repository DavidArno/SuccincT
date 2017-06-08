using SuccincT.Functional;
using SuccincT.Options;
using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal class ConsFuncMatcher<T, TResult> : IConsFuncMatcher<T, TResult>,
                                                 IConsFuncNoneHandler<T, TResult>,
                                                 IConsFuncSingleHandler<T, TResult>,
                                                 IConsFuncSingleWhereHandler<T, TResult>,
                                                 IConsFuncConsHandler<T, TResult>,
                                                 IConsFuncConsWhereHandler<T, TResult>
    {
        private (bool supplied, TResult value) _emptyValue;
        private Func<T, bool> _singleWhereFunc;
        private Func<T, IEnumerable<T>, bool> _consWhereFunc;
        private readonly List<(Func<T, bool> testFunc, Func<T, TResult> doFunc)> _singleTests;
        private readonly List<(Func<T, IEnumerable<T>, bool> testFunc,
                               Func<T, IEnumerable<T>, TResult> doFunc)> _simpleConsTests;
        private readonly ConsNodeEnumerator<T> _enumerator;

        internal ConsFuncMatcher(IEnumerable<T> collection)
        {
            _enumerator = new ConsEnumerable<T>(collection).GetTypedEnumerator();
            _singleTests = new List<(Func<T, bool>, Func<T, TResult>)>();
            _simpleConsTests = new List<(Func<T, IEnumerable<T>, bool>, Func<T, IEnumerable<T>, TResult>)>();
        }

        IConsFuncNoneHandler<T, TResult> IConsFuncMatcher<T, TResult>.Empty() => this;
        IConsFuncSingleHandler<T, TResult> IConsFuncMatcher<T, TResult>.Single() => this;
        IConsFuncConsHandler<T, TResult> IConsFuncMatcher<T, TResult>.Cons() => this;

        TResult IConsFuncMatcher<T, TResult>.Result()
        {
            var simpleMatchData = TryCons(_enumerator);
            if (!simpleMatchData.head.HasValue)
            {
                return _emptyValue.supplied
                    ? _emptyValue.value
                    : throw new NoMatchException("No empty clause supplied");
            }

            return simpleMatchData.tail == null
                ? SingleMatch(simpleMatchData.head.Value)
                : ConsMatch(simpleMatchData.head.Value, simpleMatchData.tail);
        }

        IConsFuncMatcher<T, TResult> IConsFuncNoneHandler<T, TResult>.Do(TResult value)
        {
            _emptyValue = (true, value);
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleHandler<T, TResult>.Do(TResult value)
        {
            _singleTests.Add((_ => true, _ => value));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleHandler<T, TResult>.Do(Func<T,TResult> doFunc)
        {
            _singleTests.Add((_ => true, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleWhereHandler<T, TResult>.Do(TResult value)
        {
            _singleTests.Add((_singleWhereFunc, _ => value));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleWhereHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTests.Add((_singleWhereFunc, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncConsHandler<T, TResult>.Do(Func<T, IEnumerable<T>, TResult> doFunc)
        {
            _simpleConsTests.Add(((x, y) => true, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncConsHandler<T, TResult>.Do(TResult value)
        {
            _simpleConsTests.Add(((x, y) => true, (x, y) => value));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncConsWhereHandler<T, TResult>.Do(Func<T, IEnumerable<T>, TResult> doFunc)
        {
            _simpleConsTests.Add((_consWhereFunc, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncConsWhereHandler<T, TResult>.Do(TResult value)
        {
            _simpleConsTests.Add((_consWhereFunc, (x, y) => value));
            return this;
        }

        IConsFuncSingleWhereHandler<T, TResult> IConsFuncSingleHandler<T, TResult>.Where(Func<T, bool> testFunc)
        {
            _singleWhereFunc = testFunc;
            return this;
        }

        IConsFuncConsWhereHandler<T, TResult> IConsFuncConsHandler<T, TResult>.Where(Func<T, IEnumerable<T>,
                                                                                     bool> testFunc)
        {
            _consWhereFunc = testFunc;
            return this;
        }

        private TResult SingleMatch(T head)
        {
            if (_singleTests.Count == 0) throw new NoMatchException("No single clause supplied.");

            foreach (var (testFunc, doFunc) in _singleTests)
            {
                if (testFunc(head)) return doFunc(head);
            }

            throw new NoMatchException("No single clause matches the supplied value.");
        }

        private TResult ConsMatch(T head, IConsEnumerable<T> tail)
        {
            if (_simpleConsTests.Count == 0) throw new NoMatchException("No cons clause supplied.");

            foreach (var (testFunc, doFunc) in _simpleConsTests)
            {
                if (testFunc(head, tail)) return doFunc(head, tail);
            }

            throw new NoMatchException("No cons clause matches the supplied value.");
        }

        private static (Option<T> head, ConsEnumerable<T> tail) TryCons(ConsNodeEnumerator<T> enumerator)
        {
            if (!enumerator.MoveNext()) return (Option<T>.None(), null);

            var head = enumerator.Current;
            var tailNode = enumerator.Node.Next;
            return enumerator.MoveNext() ? (head, new ConsEnumerable<T>(tailNode)) : (head, null);
        }
    }
}