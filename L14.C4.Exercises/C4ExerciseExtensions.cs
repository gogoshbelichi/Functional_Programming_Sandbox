using L13.C4.Functors;
using static L13.C4.Functors.FunctorExt;

namespace L14.C4.Exercises;

public static class C4ExerciseExtensions
{
    public static IDictionary<TKey,TResult> Map<TKey, TValue, TResult>
        (this IDictionary<TKey, TValue> ts, Func<TValue, TResult> f) where TKey : notnull
    {
       var result =  new Dictionary<TKey,TResult>();
       foreach (var item in ts)
       {
           result[item.Key] = f(item.Value);
       }
       return result;
    }

    public static ISet<TResult> Map<T, TResult>(this ISet<T> ts, Func<T, TResult> f)
    {
        var result = new HashSet<TResult>();
        foreach (var item in ts)
        {
            result.Add(f(item));
        }
        return result;
    }

    public static Option<TResult> Map<T, TResult>
        (this Option<T> opt, Func<T, TResult> f)
        => opt.Bind<T, TResult>(o => F.Some(f(o)));
    
    public static IEnumerable<TResult> Map<T, TResult>
        (this IEnumerable<T> opt, Func<T, TResult> f)
        => opt.Bind<T, TResult>(o => List(f(o)));

    public static Option<WorkPermit> GetWorkPermit
        (Dictionary<string, Employee> people, string employeeId)
        => people.Lookup(employeeId).Bind(e => e.WorkPermit).Where(HasPermission);
    
    static Func<WorkPermit, bool> HasPermission = wp => wp.Expiry > DateTime.UtcNow.Date;
    // ex. 3
    public static double AverageYearsWorkedAtTheCompany(List<Employee> employees)
        => employees.Average(e =>
            (e.LeftOn.Match(
                none: () => DateTime.Now,
                some: dt => dt
            ) - e.JoinedOn).TotalDays / 365.25);
    // ex. 4
    public static double AverageYearsWorkedAtTheCompanyV2(List<Employee> employees)
        => employees
            .Bind(e => e.LeftOn.Map(leftOn => YearsBetween(e.JoinedOn, leftOn)))
            .Average();
    
    static double YearsBetween(DateTime start, DateTime end)
        => (end - start).Days / 365d;
}