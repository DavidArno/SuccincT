using SuccincT.Functional;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal class MapperMatcher<T, TResult> : IMapperMatcher<T, TResult>,
                                               IMapperNoneHandler<T, TResult>,
                                               IMapperSingleHandler<T, TResult>,
                                               IMapperRecursiveConsHandler<T, TResult>,
                                               IMapperSingleWhereHandler<T, TResult>,
                                               IMapperRecursiveConsWhereHandler<T, TResult>
    {
        private readonly IEnumerable<T> _collection;
        private Option<TResult> _noneValue;
        private readonly List<(Func<T, bool> whereTest, Func<T, TResult> doFunc)> _singleTestAndDos;

        private readonly List<(Func<T, T, bool> whereTest, 
                               Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc)> _consTestAndDos;

        private Func<T, bool> _singleWhereTest;
        private Func<T, T, bool> _consWhereTest;

        public MapperMatcher(IEnumerable<T> collection)
        {
            _noneValue = Option<TResult>.None();
            _collection = collection;
            _singleTestAndDos = new List<(Func<T, bool> whereTest, Func<T, TResult> doFunc)>();
            _consTestAndDos = new List<(Func<T, T, bool> whereTest, 
                                        Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc)>();
        }

        IMapperNoneHandler<T, TResult> IMapperMatcher<T, TResult>.Empty() => this;

        IMapperSingleHandler<T, TResult> IMapperMatcher<T, TResult>.Single() => this;

        IMapperRecursiveConsHandler<T, TResult> IMapperMatcher<T, TResult>.RecursiveCons() => this;

        IMapperSingleWhereHandler<T, TResult> IMapperSingleHandler<T, TResult>.Where(Func<T, bool> whereTest)
        {
            _singleWhereTest = whereTest;
            return this;
        }

        IMapperRecursiveConsWhereHandler<T, TResult> IMapperRecursiveConsHandler<T, TResult>.Where(
            Func<T, T, bool> whereFunc)
        {
            _consWhereTest = whereFunc;
            return this;
        }

        IMapperMatcher<T, TResult> IMapperNoneHandler<T, TResult>.Do(TResult doValue)
        {
            _noneValue = doValue;
            return this;
        }

        IMapperMatcher<T, TResult> IMapperSingleHandler<T, TResult>.Do(TResult doValue)
        {
            _singleTestAndDos.Add((_ => true, _ => doValue));
            return this;
        }

        IMapperMatcher<T, TResult> IMapperSingleHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTestAndDos.Add((_ => true, doFunc));
            return this;
        }

        IMapperMatcher<T, TResult> IMapperSingleWhereHandler<T, TResult>.Do(TResult doValue)
        {
            _singleTestAndDos.Add((_singleWhereTest, _ => doValue));
            return this;
        }

        IMapperMatcher<T, TResult> IMapperSingleWhereHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTestAndDos.Add((_singleWhereTest, doFunc));
            return this;
        }

        IMapperMatcher<T, TResult> IMapperRecursiveConsHandler<T, TResult>.Do(
            Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc)
        {
            _consTestAndDos.Add(((_, __) => true, doFunc));
            return this;
        }

        IMapperMatcher<T, TResult> IMapperRecursiveConsWhereHandler<T, TResult>.Do(
            Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc)
        {
            _consTestAndDos.Add((_consWhereTest, doFunc));
            return this;
        }

        IEnumerable<TResult> IMapperMatcher<T, TResult>.Result() =>
            !_collection.Any() ? HandleEmptyCollection() : MapCollection();

        private IEnumerable<TResult> HandleEmptyCollection() => _noneValue.HasValue
            ? new ConsEnumerable<TResult>(_noneValue.Value)
            : throw new NoMatchException("No empty clause supplied when handling an empty collection");

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private IEnumerable<TResult> MapCollection()
        {
            var reversedCollection = _collection is IList<T> list
                ? YieldReversedList(list)
                : _collection.Reverse();

            var firstElement = reversedCollection.First();
            var (resultCollection, successfulMatch) = HandleFirstElement(firstElement);

            if (!successfulMatch)
            {
                throw new NoMatchException("No matching Single clause found for multiple element collection");
            }

            var consElements = reversedCollection.Skip(1);
            var last = firstElement;

            foreach (var item in consElements)
            {
                (resultCollection, successfulMatch) = HandleConsElement(item, last, resultCollection);
                if (!successfulMatch)
                {
                    throw new NoMatchException("No matching RecursiveCons clause found for multiple element collection");
                }
                last = item;
            }

            return resultCollection;
        }

        private (IConsEnumerable<TResult> result, bool successfullMatch) HandleFirstElement(T firstElement)
        {
            foreach (var (testFunc, doFunc) in _singleTestAndDos)
            {
                if (testFunc(firstElement)) return (new ConsEnumerable<TResult>(doFunc(firstElement)), true);
            }

            return (null, false);
        }

        private (IConsEnumerable<TResult> result, bool successfullMatch) HandleConsElement(
            T element,
            T last,
            IConsEnumerable<TResult> mappedCollection)
        {
            foreach (var (testFunc, doFunc) in _consTestAndDos)
            {
                if (testFunc(element, last)) return (doFunc(element, last, mappedCollection), true);
            }

            return (null, false);
        }

        private static IEnumerable<T> YieldReversedList(IList<T> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }
    }
}