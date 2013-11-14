using SuccincT.Unions;

namespace SuccincT.Options
{
    public class Option<T> : Union<T, None>
    {
        public Option(T value) : base(value) { }
        internal Option(None value) : base(value) { }

        public bool HasValue { get { return Case == Variant.Case1; } }
        public T Value { get { return Case1; } }
    }
}
