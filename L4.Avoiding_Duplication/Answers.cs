namespace L4.Avoiding_Duplication;

public static class Answers
{
    public static readonly Func<int, bool> IsEven = x => x % 2 == 0;

    public static Func<T, bool> Negate<T>(Func<T, bool> func) => x => !func(x);
    
    // 3rd question answer - from book answers
    public static List<int> QuickSort(this List<int> list)
    {
        if (list.Count == 0) return new List<int>();

        var pivot = list[0];
        var rest = list.Skip(1);

        var small = from item in rest where item <= pivot select item;
        var large = from item in rest where pivot < item select item;

        return small.ToList().QuickSort()
            .Append(pivot)
            .Concat(large.ToList().QuickSort())
            .ToList();
    }
    
    // 4th question answer - from book answers
    static List<T> QuickSort<T>(this List<T> list, Comparison<T> compare)
    {
        if (list.Count == 0) return new List<T>();

        var pivot = list[0];
        var rest = list.Skip(1);

        var small = from item in rest where compare(item, pivot) <= 0 select item;
        var large = from item in rest where 0 < compare(item, pivot) select item;

        return small.ToList().QuickSort(compare)
            .Concat(new List<T> { pivot })
            .Concat(large.ToList().QuickSort(compare))
            .ToList();
    }
}