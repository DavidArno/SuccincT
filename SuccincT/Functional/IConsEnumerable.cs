using System.Collections.Generic;

namespace SuccincT.Functional
{
    public interface IConsEnumerable<T> : IEnumerable<T>
    {
        ConsResult<T> Cons();

        IConsEnumerable<T> Cons(T head);
        IConsEnumerable<T> Cons(IEnumerable<T> head);
    }
}
