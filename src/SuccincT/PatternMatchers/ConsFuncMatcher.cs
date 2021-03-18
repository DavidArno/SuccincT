using SuccincT.Functional;
using SuccincT.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    internal sealed class ConsFuncMatcher<T, TResult> : IConsFuncMatcher<T, TResult>,
                                                 IConsFuncNoneHandler<T, TResult>,
                                                 IConsFuncSingleHandler<T, TResult>,
                                                 IConsFuncSingleWhereHandler<T, TResult>,
                                                 IConsFuncConsHandler<T, TResult>,
                                                 IConsFuncConsWhereHandler<T, TResult>,
                                                 IConsFuncRecursiveConsHandler<T, TResult>,
                                                 IConsFuncRecursiveConsWhereHandler<T, TResult>
       
    {
        private (bool supplied, TResult value) _emptyValue;
        private Func<T, bool>? _singleWhereFunc;
        private Func<T, IEnumerable<T>, bool>? _consWhereFunc;

        private readonly List<(Func<T, bool> testFunc, Func<T, TResult> doFunc)> _singleTests;

        private readonly List<(Func<T, IEnumerable<T>, bool> testFunc,
                               Func<T, IEnumerable<T>, TResult> doFunc)> _simpleConsTests;

        private Func<T, bool>? _recursiveConsWhereFunc;
        private readonly List<(Func<T, bool> testFunc,
                               Func<T, TResult, TResult> doFunc)> _recursiveConsTests;

        private readonly ConsNodeEnumerator<T> _enumerator;
        private readonly IEnumerable<T> _collection;

        internal ConsFuncMatcher(IEnumerable<T> collection)
        {
            _consWhereFunc = null;
            _singleWhereFunc = null;
            _recursiveConsWhereFunc = null;

            _collection = collection;
            _enumerator = new ConsEnumerable<T>(_collection).GetTypedEnumerator();
            _singleTests = new List<(Func<T, bool>, Func<T, TResult>)>();
            _simpleConsTests = new List<(Func<T, IEnumerable<T>, bool>, Func<T, IEnumerable<T>, TResult>)>();
            _recursiveConsTests = new List<(Func<T, bool> testFunc, Func<T, TResult, TResult> doFunc)>();
        }

        IConsFuncNoneHandler<T, TResult> IConsFuncMatcher<T, TResult>.Empty() => this;
        IConsFuncSingleHandler<T, TResult> IConsFuncMatcher<T, TResult>.Single() => this;
        IConsFuncConsHandler<T, TResult> IConsFuncMatcher<T, TResult>.Cons() => this;
        IConsFuncRecursiveConsHandler<T, TResult> IConsFuncMatcher<T, TResult>.RecursiveCons() => this;

        TResult IConsFuncMatcher<T, TResult>.Result()
        {
            var simpleMatchData = TryCons(_enumerator);
            if (!simpleMatchData.head.HasValue)
            {
                return _emptyValue.supplied
                    ? _emptyValue.value
                    : throw new NoMatchException("No empty clause supplied");
            }

            return _recursiveConsTests.Any() ? CalculateRecursiveResult() : CalculateSimpleResult(simpleMatchData);
        }

        private TResult CalculateRecursiveResult()
        {
            var list = _collection is IList<T> collectionList ? collectionList : _collection.ToList();
            var singleTestsSupplied = _singleTests.Count > 0;
            var result = _emptyValue.supplied ? _emptyValue.value : default;
            result = singleTestsSupplied ? SingleMatch(list[list.Count - 1]) : result;

            for (var i = list.Count - (singleTestsSupplied ? 2 : 1); i >= 0; i--)
            {
                result = _recursiveConsTests.Where(testDo => testDo.testFunc(list[i]))
                                            .Aggregate(result!, (current, testDo) => testDo.doFunc(list[i], current));
            }

            return result!;
        }

        private TResult CalculateSimpleResult((Option<T> head, ConsEnumerable<T>? tail) simpleMatchData) =>
            simpleMatchData.tail == null
                ? SingleMatchWithEmptyCheck(simpleMatchData.head.Value)
                : ConsMatch(simpleMatchData.head.Value, simpleMatchData.tail);

        IConsFuncRecursiveConsWhereHandler<T, TResult> IConsFuncRecursiveConsHandler<T, TResult>.Where(
            Func<T, bool> testFunc)
        {
            _recursiveConsWhereFunc = testFunc;
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncRecursiveConsHandler<T, TResult>.Do(Func<T, TResult, TResult> doFunc)
        {
            _recursiveConsTests.Add((_ => true, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncRecursiveConsWhereHandler<T, TResult>.Do(TResult value)
        {
            _recursiveConsTests.Add((_recursiveConsWhereFunc!, (x, y) => value));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncRecursiveConsWhereHandler<T, TResult>.Do(Func<T, TResult, TResult> doFunc)
        {
            _recursiveConsTests.Add((_recursiveConsWhereFunc!, doFunc));
            return this;
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

        IConsFuncMatcher<T, TResult> IConsFuncSingleHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTests.Add((_ => true, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleWhereHandler<T, TResult>.Do(TResult value)
        {
            _singleTests.Add((_singleWhereFunc!, _ => value));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncSingleWhereHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTests.Add((_singleWhereFunc!, doFunc));
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
            _simpleConsTests.Add((_consWhereFunc!, doFunc));
            return this;
        }

        IConsFuncMatcher<T, TResult> IConsFuncConsWhereHandler<T, TResult>.Do(TResult value)
        {
            _simpleConsTests.Add((_consWhereFunc!, (x, y) => value));
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

        private TResult SingleMatchWithEmptyCheck(T head) =>
            _singleTests.Count > 0 ? SingleMatch(head) : throw new NoMatchException("No single clause supplied.");

        private TResult SingleMatch(T head)
        {
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

        private static (Option<T> head, ConsEnumerable<T>? tail) TryCons(ConsNodeEnumerator<T> enumerator)
        {
            if (!enumerator.MoveNext()) return (Option<T>.None(), null);

            var head = new Option<T>(enumerator.Current);
            var tailNode = enumerator.Node.Next;
            return enumerator.MoveNext() ? (head, new ConsEnumerable<T>(tailNode!)) : (head, null);
        }
    }
}