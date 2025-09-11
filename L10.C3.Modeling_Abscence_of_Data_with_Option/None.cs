namespace Option;

public readonly struct None // p.70 tell us that...
    // None doesn’t have (and doesn’t need) a type parameter T; it can’t therefore
    // implement the generic interface Option<T>. It would be nice if None could be
    // treated as an Option<T> regardless of what type the type parameter T is eventually
    // assigned, but this isn’t supported by C#’s type system.
{
    /* None has no members because it contains no data.*/
    public static readonly None Default = new();
}