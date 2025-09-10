//  Map : (C<T>, (T -> R)) -> C<R> less precise pattern

/*That is, Map can be defined as a function that takes a container C<T> and a function f
 of type (T -> R), and returns a container C<R> wrapping the value(s) resulting from
 applying f to the container’s inner value(s).
 In FP, a type for which such a Map function is defined is called a functor.*/

using L12.C4.Patterns_in_FP;using L13.C4.Functors;
using Option;
using static System.Console;
using static L12.C4.Patterns_in_FP.NewExtensions;
using static L13.C4.Functors.FunctorExt;

// Performing side effects with ForEach

new List<int>() { 1, 2, 3 }.ForEach(Write);

// if you use var (instead of Option) - you have to point on Value property and foreach traverse chars in the name
Option<string> opt = new Some<string>("John");
opt.ForEach(name => WriteLine($"Hello, {name}"));
var opt2 = new Some<string>("Adam");
opt2.Value.ForEach(name => WriteLine($"Hello, {name}"));

Option<string> name = new Some<string>("Enrico");
name.Map(n => n.ToUpper())
    .ForEach(WriteLine);

/* in Book: name.Map(String.ToUpper).ForEach(WriteLine); -> Don't work as I figured out,
   but after that I read, it is because:
   NOTE In the preceding code, Book author used LaYumba.Functional.String, a class
     that exposes some commonly used functionality of System.String through
     static methods. This allows me to refer to String.ToUpper as a function, with
     out the need to specify the instance on which the ToUpper instance method
     acts, as in: s => s.ToUpper(). */

IEnumerable<string> names = ["Constance", "Albert"];
names.Map(n => n.ToUpper())
    .ForEach(WriteLine);

string input = "10";
Option<int> optI = Int.Parse(input);
Option<Option<Age>> ageOpt = optI.Map(i => Age.Of(i));
/*  As you can see, we have a problem—we end up with a nested value: Option of Option
 of Age…How are we going to work with that? */
 
// Chaining functions with Bind
Age a = new Age(3);
WriteLine(a); // implicit convertion
WriteLine(parseAge("26").Match(
    none: () => "Can't Parse",
    some: value => $"Age is {(string)value}"
)); // => Some(26) 
var x2 = parseAge("notAnAge"); // => None 
var x3 = parseAge("180"); // => None

// Flattening nested lists with Bind

/* You’ve just seen how you can use Bind to avoid having nested Options. The same idea
 applies to lists. But what are nested lists? Two-dimensional lists! We need a function
 that will apply a list-returning function to a list. But rather than returning a two
dimensional list, it should flatten the result into a one-dimensional list. */

var neighbors = new[]
{
    new { Name = "John", Pets = new object[] {"Fluffy", "Thor"} },
    new { Name = "Tim", Pets = new object[] {} },
    new { Name = "Carl", Pets = new object[] {"Sybil"} },
};

IEnumerable<IEnumerable<object>> nested = neighbors
    .Map(n => n.Pets);
// => [["Fluffy", "Thor"], [], ["Sybil"]] 

/* Actually, it’s called a monad
 Let’s now generalize the pattern for Bind. If we use C<T> to indicate some structure
     that contains value(s) of type T, then Bind takes an instance of the container and a
     function with signature (T => C<R>) and returns a C<R>. So the signature of Bind is
     always in this form:
                        Bind : (C<T>, (T => C<R>)) => C<R>
 You saw that, for all practical purposes, functors are types for which a suitable Map
     function is defined. Similarly, monads are types for which a Bind function is defined.
     You’ll sometimes hear people talk of monadic bind to clarify that they’re not just talking
     about some function called Bind, but about the Bind function that allows the type to
     be treated as a monad. */

IEnumerable<object> flat = neighbors
    .Bind(n => n.Pets);
// => ["Fluffy", "Thor", "Sybil"]

// Return function

/*In addition to the Bind function, monads must also have a Return function that “lifts”
    a normal value T into a monadic value C<T>. For Option, this is the Some function we
    defined in chapter 3.
  What’s the Return function for IEnumerable? Well, since there are many implemen
    tations of IEnumerable, there are many possible ways to create an IEnumerable. In my
    functional library, I have a suitable Return function for IEnumerable called List.
    To stick with functional design principles, it uses an immutable implementation: */

var empty     = List<string>(); // => [] 
var singleton = List("Andrej"); // => ["Andrej"] 
var many      = List("Karina", "Natasha"); // => ["Karina", "Natasha"]

/* Map    : (C<T>, (T => R)) => C<R>
   Bind   : (C<T>, (T => C<R>)) => C<R>
   Return : T => C<T> */

// Filtering values with Where
ToNatural("2"); // Some(2)
ToNatural("-2"); // None
ToNatural("hello"); // None

// Combining Option and IEnumerable with Bind
var smth = new Some<string>("thing").ToOption().AsEnumerable().Count();
WriteLine(smth);

IEnumerable<Subject> Population =
[
    new Subject { Age = Age.Of(33) },
    new Subject { }, 
    new Subject { Age = Age.Of(37) },
];

var optionalAges = Population
    .Map(p => p.Age)
    .ForEach(p => WriteLine(p.Match(
        none:  () => "Can't Pop",
        some: value => $"Age is {(string)value}")));
// => [Some(Age(33)), None, Some(Age(37))]
var statedAges = Population.Bind(p => p.Age);
// => [Age(33), Age(37)] 
var averageAge = statedAges.Map(age => age.GetValue()).Average();
WriteLine(averageAge);
// => 35

ReadLine();

static Age ReadAge()
    => parseAge(Prompt("Please enter your age"))
        .Match(
            () => ReadAge(),
            age => age);

static string Prompt(string prompt)
{
    WriteLine(prompt);
    return ReadLine() ?? Prompt(prompt);
}

