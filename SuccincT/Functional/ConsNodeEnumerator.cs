using System;
using System.Collections;
using System.Collections.Generic;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal sealed class ConsNodeEnumerator<T> : IEnumerator<T>
    {
        private ConsNode<T> _node;

        public ConsNodeEnumerator(ConsNode<T> node)
        {
            _node = node;
        }

        public bool MoveNext()
        {
            if (!AdvanceToNextNode()) return false;

            if (_node.State == HasEnumeration)
            {
                _node.Enumerating(new ConsListBuilderEnumerator<T>(_node));
            }

            if (_node.State == HasValue) return true;

            if (_node.Enumerator.MoveNext())
            {
                var newNode = new ConsNode<T> { Next = _node.Next };
                newNode.Enumerating(_node.Enumerator);

                _node.HasValue(_node.Enumerator.Current.Value);
                _node.Next = newNode;
                return true;
            }

            _node.IgnoredNode();
            return MoveNext();
        }

        private bool AdvanceToNextNode()
        {
            _node = _node.Next;
            if (_node == null)
            {
                Dispose();
                return false;
            }

            while (_node.State == IgnoredNode)
            {
                _node = _node.Next;
                if (_node == null)
                {
                    Dispose();
                    return false;
                }
            }
            return true;
        }

        public void Reset() { throw new NotImplementedException(); }

        public T Current => _node.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}