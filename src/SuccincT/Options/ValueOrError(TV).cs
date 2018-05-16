namespace SuccincT.Options
{
    public class ValueOrError<TValue> : ValueOrError<TValue, string>
    {
        protected internal ValueOrError(TValue value, string error) : base(value, error)
        {
        }
    }
}
