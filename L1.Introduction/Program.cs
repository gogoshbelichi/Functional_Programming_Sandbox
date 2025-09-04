// Functions as first-class values

Func<int, int> triple = x => x * 3;
var range = Enumerable.Range(1, 3);
var triples = range.Select(triple);

foreach (var item in triples)
{
    Console.Write($"{item} ");
}

Console.WriteLine();

// Avoiding state mutation

int[] nums = { 1, 2, 3 };
nums[0] = 7; // => [7, 2, 3]  (Purely functional languages don’t allow in-place updates at all.)
// But it seems to me that this approach should be applied very situationally,
// for example, when additional allocations are appropriate.

// Functional approach: Where and OrderBy don’t affect the original list

Func<int, bool> isOdd = x => x % 2 == 1; 
int[] original = { 7, 6, 1 }; 

var sorted = original.OrderBy(x => x); // - O(n) memory usage

foreach (var item in sorted)
{
    Console.Write($"{item} ");
}

Console.WriteLine();

var filtered = original.Where(isOdd);

foreach (var item in filtered)
{
    Console.Write($"{item} ");
}

Console.WriteLine();

// Nonfunctional approach: List<T>.Sort sorts the list in place

var original2 = new List<int> { 5, 7, 1 };
original2.Sort(); // => [1, 5, 7] - O(1) memory usage

foreach (var item in original2)
{
    Console.Write($"{item} ");
}

Console.WriteLine();

// Mutating state from concurrent processes yields unpredictable results

var nums2 = Enumerable.Range(-10000, 20001).Reverse().ToList();
// => [10000, 9999, ... , -9999, -10000] 
Action task1 = () => Console.WriteLine($"1: {nums2.Sum()}");
Action task2 = () => { nums2.Sort(); Console.WriteLine($"2: {nums2.Sum()}"); };
Parallel.Invoke(task1, task2); //NO RESULTS

Action task3 = () => Console.WriteLine($"3: {nums2.OrderBy(x => x).Sum()}");
Parallel.Invoke(task1, task3); // Predictable result

// The functional nature of LINQ
var enumerable = Enumerable.Range(1, 100)
    .Where(i => i % 20 == 0) //
    .OrderBy(i => -i) // desc
    .Select(i => $"{i}%");
// => ["100%", "80%", "60%", "40%", "20%"]
foreach (var item in enumerable)
{
    Console.Write($"{item} ");
}
Console.WriteLine();
