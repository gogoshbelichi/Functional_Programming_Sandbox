// Fact: void isn’t ideal
// I’ll introduce Unit: a type that can be used to represent
//   the absence of data, without the problems of void.
/* The problem is that although the framework has the System.Void type and the void
 keyword to represent “no return value,” Void receives special treatment by the compiler
 and can’t therefore be used as a return type (in fact, it can’t be used at all from C# code).*/

using System.Diagnostics;
using System.Text;
using Unit =  System.ValueTuple;

var path = Path.GetRelativePath(Directory.GetCurrentDirectory(), @"..\..\..\text.md");
Console.WriteLine(path);

/* If you wanted to read the contents of a file and log how long the operation took, you
 could use this function like this */
var contents = Instrumentation.Time("reading from file.md", () => File.ReadAllText(path)); 

Console.WriteLine(contents);

/* It would be quite natural to want to use this with a void-returning function. For example,
    you might want to time how long it takes to write to a file, so you’d like to write this: */

Instrumentation.Time("writing to file.md", () => File.AppendAllText(path, "NEW CONTENT!", Encoding.UTF8));
/* The problem is that AppendAllText returns void, so it can’t be represented as a Func. */
public static class Instrumentation
{
    public static T Time<T>(string op, Func<T> f)
    {
        var sw = new Stopwatch();
        sw.Start();
        
        T t = f();
        
        sw.Stop();
        Console.WriteLine($"{op} took {sw.ElapsedMilliseconds}ms"); 
        return t;
    }
    
    /*To make the preceding code work, you’d need to add an overload of Instrumentation
    .Time that takes an Action, like this:*/
    public static void Time(string op, Action act) => Time<Unit>(op, act.ToFunc());
}

public static class U
{
    public static Unit Unit() => default(Unit);
}

public static class ActionExtensions
{
    public static Func<Unit> ToFunc(this Action action)
        => () => { action(); return new Unit(); };
    
    public static Func<T, Unit> ToFunc<T>(this Action<T> action) 
        => (t) => { action(t); return new Unit(); };
}


