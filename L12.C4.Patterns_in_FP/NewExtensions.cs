using Option;

namespace L12.C4.Patterns_in_FP;

public static class NewExtensions
{
    // Mapping a function onto a sequence
    public static IEnumerable<TResult> Map<T, TResult>
        (this IEnumerable<T> ts, Func<T, TResult> f) 
        => ts.Select(f); // ==  foreach (var t in ts) yield return f(t);
    
    public static Func<int, int> Times3 = x => x * 3;
    
    // Mapping function onto Option
    public static Option<R> Map<T, R>
        (this Option<T> option, Func<T, R> function)
        => option.Match<Option<R>>(
            none: () => None.Default,
            some: t => new Some<R>(function(t)));
    
    public static Func<string, string> Greet = name => $"hello, {name}";
    
    public static Func<Apple, ApplePie> MakePie = apple => new ApplePie(apple);
    
    public static Option<Risk> RiskOf(Subject subject)
        => subject.Age.Map(CalculateRiskProfile);
    
    public static Risk CalculateRiskProfile(dynamic age)
        => (age < 60) ? Risk.Low : Risk.Medium;
}