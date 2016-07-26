using System;
using System.Collections;
using System.Collections.Generic;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal class ConsNodeEnumerator<T> : IEnumerator<T>
    {
        private ConsNode<T> _node;

        public ConsNodeEnumerator(ConsNode<T> node)
        {
            _node = node;
        }

        public bool MoveNext()
        {
            if (_node.State == IgnoredNode) _node = _node.Next;
            if (_node?.State == MarkIgnoredNextPass) _node.State = IgnoredNode;
            if (_node?.Next == null) return false;

            _node = _node.Next;

            if (_node.State == HasEnumeration)
            {
                _node.State = Enumerating;
                _node.Enumerator = new ConsListBuilderEnumerator<ConsNode<T>, T>(_node);
            }

            if (_node.State == Enumerating)
            {
                if (_node.Enumerator.MoveNext())
                {
                    var newNode = new ConsNode<T>
                    {
                        Enumerator = _node.Enumerator,
                        Next = _node.Next,
                        State = Enumerating
                    };
                    _node.Value = _node.Enumerator.Current.Value;
                    _node.Next = newNode;
                    _node.State = HasValue;
                    return true;
                }

                _node.State = MarkIgnoredNextPass;
                _node.Enumerator.Dispose();
                _node.Enumerator = null;
                return MoveNext();
            }

            return true;
        }

        public void Reset() { throw new NotImplementedException(); }

        public T Current => _node.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}