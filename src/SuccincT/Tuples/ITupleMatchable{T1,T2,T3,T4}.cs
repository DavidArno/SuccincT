namespace SuccincT.Tuples
{
    /// <summary>
    /// Defines a minimal interface to enable simple data objects/structs to be pattern matched as if they were tuples.
    /// For an arbitrary type that have four values that can be pattern matched, the type should implement this 
    /// interface and then the resultant tuple is used with the standard tuple pattern matching syntax.
    /// </summary>
    public interface ITupleMatchable<T1, T2, T3, T4>
    {
        (T1, T2, T3, T4) PropertiesToMatch { get; }
    }
}
