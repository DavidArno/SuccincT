using System;
using System.Collections;
using System.Collections.Generic;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal sealed class ConsListBuilderEnumerator<TConsNode, T> : IEnumerator<TConsNode> where
        TConsNode : ConsNode<T>, new()
    {
        private readonly IEnumerator<T> _enumerator;
        private TConsNode _node;

        internal ConsListBuilderEnumerator(TConsNode node)
        {
            _enumerator = node.Enumeration.GetEnumerator();
        }

        public bool MoveNext()
        {
            if (!_enumerator.MoveNext()) return false;

            _node = new TConsNode
            {
                Value = _enumerator.Current,
                State = HasValue
            };

            return true;
        }

        public void Reset() { throw new NotSupportedException(); }

        public TConsNode Current => _node;

        object IEnumerator.Current => Current;

        public void Dispose() => _enumerator.Dispose();
    }
}
