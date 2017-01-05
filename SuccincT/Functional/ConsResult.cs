using SuccincT.Options;

namespace SuccincT.Functional
{
    public sealed class ConsResult<T>
    {
        public Option<T> Head { get; }
        public IConsEnumerable<T> Tail { get; }

        internal ConsResult(T head, IConsEnumerable<T> tail)
        {
            Head = Option<T>.Some(head);
            Tail = tail;
        }

        internal ConsResult(IConsEnumerable<T> tail)
        {
            Head = Option<T>.None();
            Tail = tail;
        }
    }
}