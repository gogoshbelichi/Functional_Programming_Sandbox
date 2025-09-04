// Representing functions in C#
// There are several language constructs in C# that you can use to represent functions:
//     Methods
//     Delegates
//     Lambda expressions
//     Dictionaries

#region delegates

var list = Enumerable.Range(1, 10).Select(i => i * 3).ToList();

Comparison<int> alphabetically = (l, r)
    => l.ToString().CompareTo(r.ToString()); // => public delegate int Comparison<in T>(T x, T y);

list.Sort(alphabetically);

foreach (var item in list)
{
    Console.Write(item + " ");
}

Console.WriteLine();
// If your function is short and you don’t need to reuse it elsewhere, lambdas offer the
//    most attractive notation. Also notice that in the preceding example, the compiler not
//    only infers the types of x and y to be int, it also converts the lambda to the delegate
//    type Comparison<int> expected by the Sort method, given that the provided lambda
//    is compatible with this type.

list.Sort((l, r) => l.ToString().CompareTo(r.ToString()));

#endregion

#region lambdas + closures

// Just like methods, delegates and lambdas have access to the variables in the scope
//    in which they’re declared. This is particularly useful when leveraging closures in
//    lambda expressions.5 Here’s an example.

var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
// => [Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday] 
IEnumerable<DayOfWeek> DaysStartingWith(string pattern)
    => days.Where(d => d.ToString().StartsWith(pattern));

var sDays = DaysStartingWith("S"); // => [Sunday, Saturday]

foreach (var day in sDays)
{
    Console.Write(day + " ");
}

Console.WriteLine();

// In this example, Where expects a function that takes a DayOfWeek and returns a bool.
//    In reality, the function expressed by the lambda expression also uses the value of pattern,
//    which is captured in a closure, to calculate its result.

// A closure is the combination of the lambda expression itself along with the context in which that lambda is
//    declared (that is, all the variables available in the scope where the lambda appears).

#endregion

#region dictionaries

var frenchFor = new Dictionary<bool, string>
{
    [true] = "Vrai", 
    [false] = "Faux",
};

Console.WriteLine(frenchFor[true]); 

#endregion