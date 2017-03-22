using SuccincT.Options;

namespace SuccincT.Functional
{
    public sealed class ConsResult<T>
    {
        public Option<T> Head { get; }
        public IConsEnumerable<T> Tail { get; }

        internal ConsResult((Option<T> head, IConsEnumerable<T> tail) tuple)
        {
            Head = tuple.head;
            Tail = tuple.tail;
        }
    }
}