using System;
using System.Collections;
using System.Collections.Generic;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal sealed class ConsNodeEnumerator<T> : IEnumerator<T>
    {
        public ConsNodeEnumerator(ConsNode<T> node) => Node = node;

        public bool MoveNext()
        {
            if (!AdvanceToNextNode()) return false;

            if (Node.State == HasEnumeration)
            {
                Node.Enumerating(new ConsListBuilderEnumerator<T>(Node));
            }

            if (Node.State == HasValue) return true;

            if (Node.Enumerator!.MoveNext())
            {
                var newNode = new ConsNode<T> { Next = Node.Next };
                newNode.Enumerating(Node.Enumerator);

                Node.HasValue(Node.Enumerator.Current.Value);
                Node.Next = newNode;
                return true;
            }

            Node.IgnoredNode();
            return MoveNext();
        }

        private bool AdvanceToNextNode()
        {
            if (Node.Next == null)
            {
                Dispose();
                return false;
            }
            Node = Node.Next;

            while (Node.State == IgnoredNode)
            {
                if (Node.Next == null)
                {
                    Dispose();
                    return false;
                }
                Node = Node.Next;
            }
            return true;
        }

        public void Reset() => throw new NotImplementedException();
        public T Current => Node!.Value;

        object? IEnumerator.Current => Current;

        public void Dispose() {}

        internal ConsNode<T> Node { get; private set; }
    }
}