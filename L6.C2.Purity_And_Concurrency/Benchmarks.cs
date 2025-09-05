using BenchmarkDotNet.Attributes;
using Bogus;

namespace L6.C2.Purity_And_Concurrency;

// later mb will make this as independent proj
public class Benchmarks
{
    private List<string> shoppingList;
    private List<string> shoppingList2;

    [GlobalSetup]
    public void Setup()
    {
        shoppingList = [
            "coffee beans", "BANANAS", "Dates", "snow Cookies",
            "summer sunglasses", "items", "tools", "hugs"
        ];

        shoppingList2 = [
            "coffee beans", "BANANAS", "Dates", "snow Cookies",
            "summer sunglasses", "items", "tools", "hugs"
        ];

        var faker = new Faker();
        for (var i = 0; i < 2_000_000; i++)
        {
            shoppingList2.Add(faker.Commerce.Product());
        }
    }

    [Benchmark]
    public void Sequential()
    {
        shoppingList.Select(StringExt.ToSentenceCase).ToList();
    }

    [Benchmark]
    public void Parallel()
    {
        shoppingList.AsParallel().Select(StringExt.ToSentenceCase).ToList();
    }
    
    [Benchmark]
    public void SequentialBigList()
    {
        shoppingList2.Select(StringExt.ToSentenceCase).ToList();
    }

    [Benchmark]
    public void ParallelBigList()
    {
        shoppingList2.AsParallel().Select(StringExt.ToSentenceCase).ToList();
    }
}