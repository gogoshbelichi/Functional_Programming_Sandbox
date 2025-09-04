Questions to Chapter 1.

1. Browse the methods of System.Linq.Enumerable (https://docs.microsoft.com/
en-us/dotnet/api/system.linq.enumerable). Which are HOFs? Which do you
think imply iterated application of the given function?
2. Write a function that negates a given predicate: whenever the given predicate
evaluates to true, the resulting function evaluates to false, and vice versa.
3. Write a method that uses quicksort to sort a List<int> (return a new list, rather
than sorting it in place).
4. Generalize the previous implementation to take a List<T>, and additionally a
Comparison<T> delegate.
5. In this chapter, you’ve seen a Using function that takes an IDisposable and a
function of type Func<TDisp, R>. Write an overload of Using that takes a
Func<IDisposable> as the first parameter, instead of the IDisposable. (This
can be used to avoid warnings raised by some code analysis tools about instantiating 
an IDisposable and not disposing it.) 

Answers:

1. Какие методы Enumerable — HOF?
Если открыть документацию System.Linq.Enumerable,
то высшими функциями будут те, которые принимают функцию как аргумент. Например:
Select<TSource, TResult>(this IEnumerable<TSource>, Func<TSource, TResult>)
SelectMany<TSource, TResult>(..., Func<TSource, IEnumerable<TResult>>)
Where<TSource>(..., Func<TSource, bool>)
GroupBy<TSource, TKey>(..., Func<TSource, TKey>)
OrderBy<TSource, TKey>(..., Func<TSource, TKey>)
Aggregate<TSource>(..., Func<TSource, TSource, TSource>)
Aggregate<TSource, TAccumulate>(..., Func<TAccumulate, TSource, TAccumulate>)
All, Any, Count, Contains (они принимают предикат)
DistinctBy<TSource, TKey>(..., Func<TSource, TKey>) (начиная с .NET 6)
UnionBy, IntersectBy, ExceptBy — аналогично.
Zip<TFirst, TSecond, TResult>(..., Func<TFirst, TSecond, TResult>)
Все они HOF, потому что внутрь передаётся функция (Func)

Методы вроде ToList, ToArray, First, Last, ElementAt — не HOF, потому что не принимают функции.

 Какие подразумевают итеративное применение?
Те, которые многократно вызывают переданную функцию для каждого элемента последовательности:
Where → многократно вызывает predicate для каждого элемента.
Select → многократно вызывает selector.
SelectMany → ещё более явно разворачивает вложенные последовательности.
Aggregate → типичный свёртка/итеративное применение.
GroupBy → вызывает keySelector для каждого элемента.
OrderBy → вызывает keySelector для каждого элемента (а потом ещё сортирует).
Zip → вызывает resultSelector для каждой пары элементов.
All, Any, Count(predicate) → тоже итерируются, пока не находят нужный результат.

 Какие не предполагают итеративного применения функции
ToList, ToArray — просто материализация (хотя внутри всё равно итерация коллекции, но не по твоей функции).
First, Last, ElementAt — выбирают один элемент.
Concat, Union, Append, Prepend — комбинируют коллекции, без вызова твоих функций.

2. Negate.cs
