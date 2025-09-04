using System.Diagnostics;
using BenchmarkDotNet.Running;
using Bogus;
using L6.C2.Purity_And_Concurrency;

// uncomment to use
//BenchmarkRunner.Run<Benchmarks>();

//           My benchmarks - not from the book :)
//    Small list - sequential is better 8 elements
//    Big list - parallel is better 2.000.008 elements - wins 0.4-0.5 sec
//    | Method            | Mean             | Error            | StdDev           | Median           |
//    |------------------ |-----------------:|-----------------:|-----------------:|-----------------:|
//    | Sequential        |         609.9 ns |         12.05 ns |         25.67 ns |         607.8 ns |
//    | Parallel          |       7,297.6 ns |        181.20 ns |        534.27 ns |       7,182.6 ns |
//    | SequentialBigList | 478,316,564.6 ns | 10,499,293.39 ns | 30,292,864.26 ns | 472,271,750.0 ns |
//    | ParallelBigList   | 434,866,589.7 ns |  9,657,004.44 ns | 28,016,722.96 ns | 427,138,300.0 ns |
//    I'll check later benchmark setup correctness 

var largeDataSet = Enumerable.Range(1, 5_000_000).ToList();

var stopwatch = Stopwatch.StartNew();
var linqPrimes = largeDataSet.Where(IsPrime).ToList();
stopwatch.Stop();
Console.WriteLine($"LINQ Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"LINQ Prime Count: {linqPrimes.Count}");

stopwatch.Restart();
var plinqPrimes = largeDataSet.AsParallel().Where(IsPrime).ToList();
stopwatch.Stop();
Console.WriteLine($"PLINQ Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"PLINQ Prime Count: {plinqPrimes.Count}");

List<string> shoppingList = [
    "coffee beans", "BANANAS", "Dates", "snow Cookies",
    "summer sunglasses", "items", "tools", "hugs"
];

List<string> shoppingList2 = [
    "coffee beans", "BANANAS", "Dates", "snow Cookies",
    "summer sunglasses", "items", "tools", "hugs"
];

var faker = new Faker();
for (var i = 0; i < 2_000_000; i++)
{
    shoppingList2.Add(faker.Commerce.Product());
}

stopwatch.Restart();
shoppingList2.Select(StringExt.ToSentenceCase).ToList();
stopwatch.Stop();
Console.WriteLine($"LINQ Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"LINQ Count: {shoppingList2.Count}");
stopwatch.Restart();
shoppingList2.AsParallel().Select(StringExt.ToSentenceCase).ToList();
stopwatch.Stop();
Console.WriteLine($"PLINQ Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"PLINQ Count: {shoppingList2.Count}");


new ListFormatter()
    .Format(shoppingList)
    .ForEach(Console.WriteLine);
    
static bool IsPrime(int number)
{
    if (number <= 1) return false;
    for (int i = 2; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0) return false;
    }
    return true;
}

/*Parallelizing impure functions
    Let’s see what happens if we naively apply parallelization with the impure Prepend
Counter function:

list.Select(PrependCounter).ToList()
list.AsParallel().Select(PrependCounter).ToList()

Because PrependCounter increments the counter variable, the parallel version will
    have multiple threads reading and updating the counter. As is well known, ++ is not an
atomic operation, and because there’s no locking in place, we’ll lose some of the
    updates and end up with an incorrect result.
    If you test this approach with a large enough input list, you’ll get a result like this:
Expected string length 20 but was 19. Strings differ at index 0.
    Expected: "1000000. Item1000000"
But was:  "956883*/