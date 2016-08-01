using System.Collections.Generic;

namespace SuccincT.Functional
{
    internal class ConsNode<T>
    {
        internal ConsNodeState State { get; set; }
        internal IEnumerable<T> Enumeration { get; set; }
        internal T Value { get; set; }
        internal ConsNode<T> Next { get; set; }
        internal IEnumerator<ConsNode<T>> Enumerator { get; private set; }

        internal void Enumerating(IEnumerator<ConsNode<T>> enumerator)
        {
            State = ConsNodeState.Enumerating;
            Enumeration = null;
            Enumerator = enumerator;
        }

        internal void HasValue(T value)
        {
            State = ConsNodeState.HasValue;
            Value = value;
            Enumeration = null;
            Enumerator = null;
        }

        internal void IgnoredNode()
        {
            Enumerator?.Dispose();
            Enumerator = null;
            State = ConsNodeState.IgnoredNode;
        }
    }
}