// THIS TOPIC IS STILL RELEVANT, DESPITE THE FACT THAT WE HAVE NULLABILITY.

// Modeling the possible absence of data with Option

/* Option is essentially a container that wraps a value… or no value. It’s like a box that
may contain a thing, or it could be empty. The symbolic definition for Option is as
    follows:
Option<T> = None | Some(T) */

using Option;

Option<string> firstName = new Some<string>("Enrico");
Option<string> middleName = None.Default;

Console.WriteLine(Greet(firstName)); // => "Sorry, who?"  
Console.WriteLine(Greet(middleName)); // => "Hello, John

string? kek = null;
string tik = "lol";

Console.WriteLine(Greet3(tik));
Console.WriteLine(Greet3(kek));

var dict = new Dictionary<string, string>();
dict.Add("blue", "one");
var x = dict.ToLookup(p => p.Key, p => p.Value);
var y = dict.Lookup("blue");
var z = dict.Lookup("red");
Console.WriteLine(
    y.Match(
        none: () => "no blue",
        some: (color) => color.ToUpper()
    )
);
Console.WriteLine(z.Match(
        none: () => "no red",
        some: (color) => color.ToUpper()
    )
);

string Greet(Option<string> greetee)
    => greetee.Match(
        none: () => "Sorry, who?",
        some: (name) => $"Hello, {name}");

string Greet3(string? lol) 
    => lol is null ?
        "Sorry, who?" :
        $"Hello, {lol}";

public static partial class F
{
    public static None None
        => None.Default;
    /*The None value*/
    public static Some<T> Some<T>(T value)
        => new(value);
}