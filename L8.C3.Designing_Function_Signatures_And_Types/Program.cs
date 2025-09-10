#region Arrow notation

// Arrow notation f : int => string
/*
 Function signature                              C# type                                  Example
   int => string                              Func<int, string>                      (int i) => i.ToString()
   () => string                               Func<string>                           () => "hello"
   int => ()                                  Action<int>                            (int i) => WriteLine($"gimme {i}")
   () => ()                                   Action                                 () => WriteLine("Hello World!")
   (int, int) => int                          Func<int, int, int>                    (int a, int b) => a + b
*/



Console.WriteLine(CalculateRiskProfile(new Age(30))); // => Low 
Console.WriteLine(CalculateRiskProfile(new Age(70))); // => Medium
Console.WriteLine(CalculateRiskProfile(new Age(-10))); // => Exception

Risk CalculateRiskProfile(dynamic age)
    => (age < 60) ? Risk.Low : Risk.Medium;

public enum Risk { Low, Medium, High }

#endregion

#region Writing “honest” functions

/*
  That means this function is “dishonest”—what it really should say is “Give me an int, and
    I may return a Risk, or I may throw an exception instead.”
    
  In summary, a function is honest if its behavior can be predicted by its signature: it
    returns a value of the declared type; no throwing exceptions, and no null return values.
*/

public enum Gender { Female, Male }
// Composing values with tuples and objects
public class RiskV2 // wrapped as long as I don't want to move it to top-level statements
{
    public Risk CalculateRiskProfileV2(Age age, Gender gender)
    {
        var threshold = (gender == Gender.Female) ? 62 : 60;
        return (age < threshold) ? Risk.Low : Risk.Medium;
    }
}

class HealthData
{
    public Age Age { get; set; }
    public Gender Gender { get; set; }
}

/*
  Counting the number of possible instances
     can bring clarity. Once you have control over these simple values,
     it’s easy to aggregate them into more complex data objects.
  Now let’s move on to the simplest value of all: the empty tuple, or Unit.
*/

#endregion