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
                                               IMapperRecursiveConsHandler<T, TResult>
    {
        private readonly IEnumerable<T> _collection;
        private Option<TResult> _noneValue;
        private readonly List<Func<T, TResult>> _singleTestAndDos;
        private readonly List<Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>>> _consTestAndDos;

        public MapperMatcher(IEnumerable<T> collection)
        {
            _noneValue = Option<TResult>.None();
            _collection = collection;
            _singleTestAndDos = new List<Func<T, TResult>>();
            _consTestAndDos = new List<Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>>>();
        }

        IMapperNoneHandler<T, TResult> IMapperMatcher<T, TResult>.Empty() => this;

        IMapperSingleHandler<T, TResult> IMapperMatcher<T, TResult>.Single() => this;

        IMapperRecursiveConsHandler<T, TResult> IMapperMatcher<T, TResult>.RecursiveCons() => this;

        IMapperMatcher<T, TResult> IMapperNoneHandler<T, TResult>.Do(TResult doValue)
        {
            _noneValue = doValue;
            return this;
        }

        IMapperMatcher<T, TResult> IMapperSingleHandler<T, TResult>.Do(Func<T, TResult> doFunc)
        {
            _singleTestAndDos.Add(doFunc);
            return this;
        }

        IMapperMatcher<T, TResult> IMapperSingleHandler<T, TResult>.Do(TResult doValue)
        {
            _singleTestAndDos.Add(_ => doValue);
            return this;
        }

        IMapperMatcher<T, TResult> IMapperRecursiveConsHandler<T, TResult>.Do(
            Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc)
        {
            _consTestAndDos.Add(doFunc);
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
                ? YieldReversedCollection(list)
                : _collection.Reverse();

            var (resultCollection, skipFirstElement) = _singleTestAndDos.Any()
                ? (new ConsEnumerable<TResult>(_singleTestAndDos[0](reversedCollection.First())), true)
                : ((IConsEnumerable<TResult>)new ConsEnumerable<TResult>(), false);

            var itemsForConsActions = skipFirstElement ? reversedCollection.Skip(1) : reversedCollection;

            if (itemsForConsActions.Any() && !skipFirstElement)
                throw new NoMatchException("A Single matcher must be supplied for a collection of one or more items");

            var last = reversedCollection.First();
            foreach (var item in itemsForConsActions)
            {
                if (!_consTestAndDos.Any())
                    throw new NoMatchException("No RecursiveCons clause found for multiple element collection");

                resultCollection = _consTestAndDos[0](item, last, resultCollection);
            }

            return resultCollection;
        }

        private static IEnumerable<T> YieldReversedCollection(IList<T> list)
        {
            for (var i = list.Count-1; i >= 0; i--)
            {
                yield return list[i];
            }
        }
    }
}