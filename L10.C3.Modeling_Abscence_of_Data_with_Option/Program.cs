// Modeling the possible absence of data with Option

/* Option is essentially a container that wraps a value… or no value. It’s like a box that
may contain a thing, or it could be empty. The symbolic definition for Option is as
    follows:
Option<T> = None | Some(T) */

var firstName = new Some<string>("Enrico");
var middleName = new None();

public static partial class F
{
    public static None None 
        => None.Default;
    /*The None value*/
    public static Some<T> Some<T>(T value)
        => new(value);
}

/*The Some function wraps the given value into a Some. */
public struct None 
{
    /* None has no members because it contains no data.*/
    internal static readonly None Default = new();
}
public struct Some<T>
{
    /*Some simply wraps a value.*/
    internal T Value { get; } 
    internal Some(T value)
    {
        if (value is null)
            throw new ArgumentNullException(); 
        Value = value;
    }
}