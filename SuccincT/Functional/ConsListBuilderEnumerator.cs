using System;
using System.Collections;
using System.Collections.Generic;
using static SuccincT.Functional.ConsNodeState;

namespace SuccincT.Functional
{
    internal sealed class ConsListBuilderEnumerator<T> : IEnumerator<ConsNode<T>>
    {
        private readonly IEnumerator<T> _enumerator;
        private ConsNode<T> _node;

        internal ConsListBuilderEnumerator(ConsNode<T> node) => 
            _enumerator = node.Enumeration.GetEnumerator();

        public bool MoveNext()
        {
            if (!_enumerator.MoveNext()) return false;

            _node = new ConsNode<T>
            {
                Value = _enumerator.Current,
                State = HasValue
            };

            return true;
        }

        public void Reset() => throw new NotSupportedException();
        public ConsNode<T> Current => _node;

        object IEnumerator.Current => Current;

        public void Dispose() => _enumerator.Dispose();
    }
}
