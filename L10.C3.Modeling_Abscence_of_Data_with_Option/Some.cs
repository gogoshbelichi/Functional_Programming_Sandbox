namespace Option;
/*The Some function wraps the given value into a Some. */
public readonly struct Some<T> /*: IOption<T>*/
{
    /*Some simply wraps a value.*/
    public T Value { get; } 
    public Some(T value)
    {
        if (value is null)
            throw new ArgumentNullException(); 
        Value = value;
    }
}