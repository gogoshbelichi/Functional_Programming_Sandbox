using Option;

public struct Option<T> 
{
    bool isSome { get; }
    T Value { get; }
    private Option(T value)
    {
        this.isSome = true;
        this.Value = value;
    }
    
    public static implicit operator Option<T>(None _)
        => new();
    
    public static implicit operator Option<T>(Some<T> some)
        => new(some.Value);
    
    public static implicit operator Option<T>(T value) 
        => value == null ? None.Default : new Some<T>(value);
    
    public R Match<R>(Func<R> none, Func<T, R> some) 
        => isSome ? some(Value) : none();
}

public static class OptionExtension
{
    public static Option<T> Lookup<K, T>(this IDictionary<K, T> dict, K key)
        => dict.TryGetValue(key, out var value)
            ? new Some<T>(value) : None.Default;
}