using System;
using System.Collections;
using System.Collections.Generic;
using SuccincT.Options;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal sealed class ConsEnumerable<T> : IConsEnumerable<T>
    {
        internal static readonly IConsEnumerable<T> EmptyEnumerable = new ConsEnumerable<T>();
        private readonly ConsNode<T> _node;

        internal ConsEnumerable(IEnumerable<T> enumeration) =>
            _node = new ConsNode<T>
            {
                State = StartNode,
                Next = EnumerationConsNode(enumeration)
            };

        private ConsEnumerable(IEnumerable<T> enumeration, T head) =>
            _node = new ConsNode<T>
            {
                State = StartNode,
                Next = new ConsNode<T>
                {
                    Value = head,
                    State = HasValue,
                    Next = EnumerationConsNode(enumeration)
                }
            };

        private ConsEnumerable(IEnumerable<T> enumeration, IEnumerable<T> head) =>
            _node = new ConsNode<T>
            {
                State = StartNode,
                Next = new ConsNode<T>
                {
                    Enumeration = head,
                    State = HasEnumeration,
                    Next = EnumerationConsNode(enumeration)
                }
            };

        private ConsEnumerable(ConsNode<T> node) =>
            _node = new ConsNode<T>
            {
                State = StartNode,
                Next = node
            };

        private ConsEnumerable() =>
            _node = new ConsNode<T>
            {
                State = StartNode
            };

        private IEnumerator<T> GetEnumerator() => new ConsNodeEnumerator<T>(_node);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IConsEnumerable<T> IConsEnumerable<T>.Cons(T head) => new ConsEnumerable<T>(this, head);

        IConsEnumerable<T> IConsEnumerable<T>.Cons(IEnumerable<T> head) =>
            new ConsEnumerable<T>(this, head);

        ConsResult<T> IConsEnumerable<T>.Cons()
        {
            using (var enumerator = GetEnumerator() as ConsNodeEnumerator<T>)
            {
                return enumerator.MoveNext()
                    ? new ConsResult<T>(enumerator.Current, new ConsEnumerable<T>(enumerator.Node.Next))
                    : new ConsResult<T>(Option<T>.None());
            }
        }

        internal (T head, IConsEnumerable<T> tail) TupleCons()
        {
            using (var enumerator = GetEnumerator() as ConsNodeEnumerator<T>)
            {
                return enumerator.MoveNext()
                    ? (enumerator.Current, new ConsEnumerable<T>(enumerator.Node.Next))
                    : throw new InvalidOperationException("Enumeration is empty and cannot be split into a head & tail");
            }
        }

        private static ConsNode<T> EnumerationConsNode(IEnumerable<T> enumeration) =>
            new ConsNode<T>
            {
                State = HasEnumeration,
                Enumeration = enumeration
            };
    }
}
