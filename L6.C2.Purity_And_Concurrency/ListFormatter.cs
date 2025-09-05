using L6.C2.Purity_And_Concurrency;

class ListFormatter
{
    int counter; // global state mutations
    string PrependCounter(string s) => $"{++counter}. {s}";  // impure
    
    public List<string> Format(List<string> list) //pure + impure
        => list
            .Select(StringExt.ToSentenceCase)
            .Select(PrependCounter)          
            .ToList();
    
    public static List<string> FormatZip(List<string> list) => 
        list
            .Select(StringExt.ToSentenceCase)
            .Zip(Enumerable.Range(1, list.Count), (s, i) => $"{i}. {s}")  // fully pure - no global state variables
            .ToList();
}

/* The case for static methods
 When all variables required within a method are provided as input (or are statically
 available), the method can be made static. This chapter contains several examples
 of refactoring instance methods to static methods.
 You may feel uneasy about this, especially if—like me—you’ve seen programs
 become difficult to test and maintain because of the excessive use of static classes.
 Static methods can cause problems if they do either of the following:
      Act on mutable static fields—These are effectively the most global variables,
     and it’s well known that maintainability suffers from the presence of global
     mutable variables.
      Perform I/O—In this case, it’s testability that’s jeopardized. If method A
     depends on the I/O behavior of static method B, it’s not possible to unit test A.
 Note that both these cases imply an impure function. On the other hand, when a func
tion is pure, there’s no downside to making it static. As a general guideline,
      Make pure functions static.
      Avoid mutable static fields.
      Avoid direct calls to static methods that perform I/O.
 As you code more functionally, more of your functions will be pure, so potentially more
 of your code will be in static classes without causing any problems related to the
 abuse of static classes. */