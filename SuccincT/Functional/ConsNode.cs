using System.Collections.Generic;

namespace SuccincT.Functional
{
    internal class ConsNode<T>
    {
        internal ConsNodeState State { get; set; }
        internal IEnumerable<T> Enumeration { get; set; }
        internal T Value { get; set; }
        internal ConsNode<T> Next { get; set; }
        internal IEnumerator<ConsNode<T>> Enumerator { get; set; }
    }
}