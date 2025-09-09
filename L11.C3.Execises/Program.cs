// My first try returns value (not none or some)

using System.Collections;
using System.Text.RegularExpressions;
using Option;

EnumExt.Parse<DayOfWeek>("Sunday", out var day0);
Console.WriteLine(day0); // Sunday
EnumExt.Parse<DayOfWeek>("1", out var day1);
Console.WriteLine(day1); // Monday
EnumExt.Parse<DayOfWeek>("daytwo", out var day2);
Console.WriteLine(day2); // Sunday (bad solution)

Console.WriteLine("------------------------------");

// After refactoring
Console.WriteLine(EnumExt.Parse<DayOfWeek>("Sunday").Match(
    none: () => "It's not a day of week",
    some: (value) => value.ToString())); // Sunday
Console.WriteLine(EnumExt.Parse<DayOfWeek>("1").Match(
    none: () => "It's not a day of week",
    some: (value) => value.ToString())); // Monday
Console.WriteLine(EnumExt.Parse<DayOfWeek>("2").Match(
    none: () => "It's not a day of week",
    some: (value) => value.ToString())); // Tuesday
Console.WriteLine(EnumExt.Parse<DayOfWeek>("daytwo").Match(
    none: () => "It's not a day of week",
    some: (value) => value.ToString())); // It's not a day of week

Console.WriteLine("------------------------------");

// C# parsing
Console.WriteLine(Enum.Parse<DayOfWeek>("Monday")); // Monday
Console.WriteLine(Enum.Parse<DayOfWeek>("Sunday")); // Sunday
Console.WriteLine(Enum.Parse<DayOfWeek>("5")); // Friday
//Console.WriteLine(Enum.Parse<DayOfWeek>("what")); // exception


Console.WriteLine(new List<int>().OptionLookUp(isOdd).Match(
    none: () => "Empty list",
    some: (value) => value.ToString()));
var someOption = new List<int> { 2, 3, 5 }.OptionLookUp(isOdd);
Console.WriteLine(someOption.Match(
    none: () => "No odds",
    some: (value) => value.ToString()));

Console.WriteLine(new List<int>().BookOptionLookup(isOdd).Match(
    none: () => "Empty list2",
    some: (value) => value.ToString()));
var someOption2 = new List<int> { 2, 3, 5 }.BookOptionLookup(isOdd);
Console.WriteLine(someOption2.Match(
    none: () => "No odds2",
    some: (value) => value.ToString()));

var email = Email.Create("john.doe@gmail.com");
Console.WriteLine(email.Match(
    none:  () => "No email",
    some: value => value)); // john.doe@gmail.com
var notemail = Email.Create("john.doe");
Console.WriteLine(notemail.Match(
    none:  () => "No email",
    some: value => value)); // No email

var source = new List<double>();
/* Take a look at the extension methods defined on IEnumerable inSystem.LINQ
    .Enumerable.8 Which ones could potentially return nothing, or throw some
    kind of not-found exception, and would therefore be good candidates for
    returning an Option<T> instead? 
Answer:                                                                     */
var a = Enumerable.First(source); // exceptions
                                         // ArgumentNullException — if source is null
                                         // InvalidOperationException - if source have no elements
var b = Enumerable.Last(source); 
var c = Enumerable.Max(source);
var d = Enumerable.Min(source);
var e = Enumerable.Single(source);
var f = Enumerable.Average(source);
var g = Enumerable.ElementAt(source, 5);
var h = Enumerable.Aggregate(source, (y, x) => y + x);

/*IEnumerable<Employee> employees =
[
    new Employee("Alice", "HR", 5000m),
    new Employee("Bob", "IT", 7000m),
    new Employee("Charlie", "IT", 8000m),
    new Employee("Diana", "HR", 6000m)
];

// it's works nice, no problems - idea is lean op of aggregation + group by
var departmentSalaries = employees.AggregateBy( 
    keySelector: emp => emp.Department,
    seed: 0m, // начальное значение (decimal) для каждой группы
    func: (total, emp) => total + emp.Salary
);

foreach (var kvp in departmentSalaries)
{
    Console.WriteLine($"Department: {kvp.Key}, Total Salary: {kvp.Value:C}");
}*/
//...

bool isOdd(int i) => i % 2 != 0;
record Employee(string Name, string Department, decimal Salary);



/* Exercise 1. Write a generic function that takes a string and parses it as a value of an enum. It
  should be usable as follows:
    Enum.Parse<DayOfWeek>("Friday")  // => Some(DayOfWeek.Friday) 
    Enum.Parse<DayOfWeek>("Freeday") // => None */
public static class EnumExt
{
    // my first idea (bad solution)
    public static Option<R> Parse<R>(this string value, out R result)  where R : struct
         => Enum.TryParse(value, out result) 
             ? new Option.Some<R>(result) 
             : new Option.None();
    
    // refactored
    public static Option<R> Parse<R>(this string value)  where R : struct
        => Enum.TryParse<R>(value, out var result) 
            ? new Option.Some<R>(result) 
            : new Option.None();
}

/* Exercise 2. Write a Lookup function that will take an IEnumerable and a predicate, and
     return the first element in the IEnumerable that matches the predicate, or None
     if no matching element is found. Write its signature in arrow notation:
         bool isOdd(int i) => i % 2 == 1;
         new List<int>().Lookup(isOdd)    // => None 
         new List<int> { 1 }.Lookup(isOdd) // => Some(1) */

public static class EnumerableExt
{
    public static Option<T> OptionLookUp<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        try
        {
            return new Some<T>(enumerable.First(predicate));
        }
        catch
        {
            return None.Default;
        }
    }
    
    public static Option<T> BookOptionLookup<T>(this IEnumerable<T> ts, Func<T, bool> pred)
    {
        foreach (T t in ts) if (pred(t)) return new Some<T>(t);
        return None.Default;
    }
}

/* Exercise 3. Write a type Email that wraps an underlying string, enforcing that it’s in a valid
     format. Ensure that you include the following:
         A smart constructor
         Implicit conversion to string, so that it can easily be used with the typical API for sending emails */

public partial class Email
{ 
    private static readonly Regex EmailRegex = MyRegex();
    private string Value { get; } 
    
    private Email(string value) => Value = value;
    
    public static implicit operator string(Email e)
        => e.Value;
    public static Option<Email> Create(string value) 
        => EmailRegex.IsMatch(value) 
            ? new Some<Email>(new Email(value)) 
            : None.Default;
    
    // https://regex101.com/r/afOVcK/1 reference
    [GeneratedRegex(
        @"^([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22))*\x40([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d))*$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled,
        "en-US")]
    private static partial Regex MyRegex();
}



