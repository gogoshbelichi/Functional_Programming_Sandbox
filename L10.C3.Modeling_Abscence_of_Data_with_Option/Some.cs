namespace Option;
/*The Some function wraps the given value into a Some. */
public struct Some<T> /*: IOption<T>*/
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