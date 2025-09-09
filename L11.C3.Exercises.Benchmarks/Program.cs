using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
    private bool isOdd(int i) => i % 2 != 0;
    private List<int> emptyList;
    private List<int> list;

    [GlobalSetup]
    public void Setup()
    {
        emptyList = [];
        list = [2, 4, 4, 4, 6, 10, 8, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 43];
    }

    [Benchmark]
    public void EmptyListTryCatch()
    {
        emptyList.OptionLookUp(isOdd).Match(
            none: () => "Empty list",
            some: (value) => value.ToString());
    }

    [Benchmark]
    public void ListTryCatch()
    {
        list.OptionLookUp(isOdd).Match(
            none: () => "No odds",
            some: (value) => value.ToString());
    }
    
    [Benchmark]
    public void EmptyListForeach()
    {
        emptyList.BookOptionLookup(isOdd).Match(
            none: () => "Empty list2",
            some: (value) => value.ToString());
    }

    [Benchmark]
    public void ListForeach()
    {
        list.BookOptionLookup(isOdd).Match(
            none: () => "No odds2",
            some: (value) => value.ToString());
    }
}