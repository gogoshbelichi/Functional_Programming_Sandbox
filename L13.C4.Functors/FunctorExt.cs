using System.Collections.Immutable;
using L12.C4.Patterns_in_FP;
using Option;
using Unit =  System.ValueTuple;

namespace L13.C4.Functors;

public static class FunctorExt
{
    public static IEnumerable<Unit> ForEach<T>
        (this IEnumerable<T> ts, Action<T> action)
        => ts.Map(action.ToFunc()).ToImmutableList();
    
    public static Option<Unit> ForEach<T>
        (this Option<T> opt, Action<T> action)
        => opt.Map(action.ToFunc());
    
    public static Option<R> Bind<T, R>
        (this Option<T> optT, Func<T, Option<R>> f)  // Bind takes an Option returning function.
        => optT.Match(
            () => None.Default,
            (t) => f(t));
    
    public static Func<string, Option<Age>> parseAge = s
        => Int.Parse(s).Bind(Age.Of);
    
    public static IEnumerable<R> Bind<T, R>
        (this IEnumerable<T> ts, Func<T, IEnumerable<R>> f)
    {
        return ts.SelectMany(f); 
        /* is the same as => foreach (T t in ts)
               foreach (R r in f(t))
                   yield return r; */
    }
    
    public static IEnumerable<T> List<T>(params T[] items)
        => items.ToImmutableList();
    
    public static Option<T> Where<T>
        (this Option<T> optT, Func<T, bool> pred)
        => optT.Match(
            () => None.Default,
            (t) => pred(t) ? optT : None.Default);
    
    public static bool IsNatural(int i) => i >= 0;
    
    public static Option<int> ToNatural(string s) => Int.Parse(s).Where(IsNatural);
    
    public static IEnumerable<R> Bind<T, R>
        (this IEnumerable<T> list, Func<T, Option<R>> func)
        => list.Bind(t => func(t).AsEnumerable());
    
    public static IEnumerable<R> Bind<T, R>
        (this Option<T> opt, Func<T, IEnumerable<R>> func)
        => opt.AsEnumerable().Bind(func);
}