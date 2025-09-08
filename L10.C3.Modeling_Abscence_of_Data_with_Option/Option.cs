using Option;

public struct Option<T> 
{
    bool isSome { get; }
    T value { get; }
    private Option(T value)
    {
        this.isSome = true;
        this.value = value;
    }
    
    public static implicit operator Option<T>(None _)
        => new();
    
    public static implicit operator Option<T>(Some<T> some)
        => new(some.Value);
    
    public static implicit operator Option<T>(T value) 
        => value == null ? new None() : new Some<T>(value);
    
    public R Match<R>(Func<R> none, Func<T, R> some) 
        => isSome ? some(value) : none();
}

public static class OptionExtension
{
    public static Option<T> Lookup<K, T>(this IDictionary<K, T> dict, K key)
    {
        T value;
        return dict.TryGetValue(key, out value)
            ? new Some<T>(value) : new None();
    }
}