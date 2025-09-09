// My first try returns value (not none or some)

using System.Collections;
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

bool isOdd(int i) => i % 2 != 0;

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

