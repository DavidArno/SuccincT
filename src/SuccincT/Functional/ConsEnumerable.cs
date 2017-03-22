using System.Collections;
using System.Collections.Generic;
using SuccincT.Options;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal sealed class ConsEnumerable<T> : IConsEnumerable<T>
    {
        private readonly ConsNode<T> _node;

        internal ConsEnumerable(IEnumerable<T> enumeration) =>
            _node = new ConsNode<T>
            {
                State = StartNode,
                Next = new ConsNode<T>
                {
                    State = HasEnumeration,
                    Enumeration = enumeration
                }
            };

        private ConsEnumerable(IEnumerable<T> enumeration, T head) =>
            _node = new ConsNode<T>
            {
                State = StartNode,
                Next = new ConsNode<T>
                {
                    Value = head,
                    State = HasValue,
                    Next = new ConsNode<T>
                    {
                        Enumeration = enumeration,
                        State = HasEnumeration
                    }
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
                    Next = new ConsNode<T>
                    {
                        Enumeration = enumeration,
                        State = HasEnumeration
                    }
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

        public IEnumerator<T> GetEnumerator() => new ConsNodeEnumerator<T>(_node);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IConsEnumerable<T> IConsEnumerable<T>.Cons(T head) => new ConsEnumerable<T>(this, head);

        IConsEnumerable<T> IConsEnumerable<T>.Cons(IEnumerable<T> head) => 
            new ConsEnumerable<T>(this, head);

        ConsResult<T> IConsEnumerable<T>.Cons() => new ConsResult<T>(TupleCons());

        internal (Option<T> head, IConsEnumerable<T> tail) TupleCons()
        {
            GetEnumerator().MoveNext();
            return _node.Next == null || _node.Next.State == IgnoredNode && _node.Next.Next == null
                ? (Option<T>.None(), new ConsEnumerable<T>())
                : (_node.Next.Value, new ConsEnumerable<T>(_node.Next.Next));
        }
    }
}
