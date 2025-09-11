using Option;

public readonly struct Option<T> 
{
    bool IsSome { get; }
    T Value { get; }
    private Option(T value)
    {
        this.IsSome = true;
        this.Value = value;
    }
    
    public static implicit operator Option<T>(None _)
        => new();
    
    public static implicit operator Option<T>(Some<T> some)
        => new(some.Value);
    
    public static implicit operator Option<T>(T value) 
        => value == null ? None.Default : new Some<T>(value);
    
    public R Match<R>(Func<R> none, Func<T, R> some) 
        => IsSome ? some(Value) : none();
    
    // Chapter 4 addition
    public IEnumerable<T> AsEnumerable()
    {
        if (IsSome) yield return Value;
    }
}

public static class OptionExtensions
{
    public static Option<T> Lookup<K, T>(this IDictionary<K, T> dict, K key)
        => dict.TryGetValue(key, out var value)
            ? new Some<T>(value) : None.Default;
    
    public static Option<T> ToOption<T>
        (this Some<T> some) 
        => some.Value;
}