// HOFs are functions that take other functions as inputs or return a function as out put, or both.
//     I’ll assume that you’ve already used HOFs to some extent, such as with LINQ.

#region Functions that depend on other functions

/*                Functions that depend on other functions
   You’ve seen some examples of such HOFs earlier in this chapter: Sort (an
      instance method on List) and Where (an extension method on IEnumerable).
      List.Sort, when called with a Comparison delegate, is a method that says: “OK, I’ll
      sort myself, as long as you tell me how to compare any two elements that I contain.”
      Sort does the job of sorting, but the caller can decide what logic to use for comparing.

       Let’s look at an idealized implementation of Where.6
 Listing 1.10 Where: a typical HOF that iteratively applies the given predicate

 The task of iterating over the list is an                                      The criterion determining which items
  implementation detail of Where.                                                are included is decided by the caller.
                         |                                                               |
                         |        public static IEnumerable<T> Where<T>                  |
                         |          (this IEnumerable<T> ts, Func<T, bool> predicate)    |
                         |        {                                                      |
                         L ---------->  foreach (T t in ts)                              |
                                            if (predicate(t))  <------------------------_|
                                            yield return t;
                                 }

                    HOF (f(), args) { for(){ f() } } or HOF (f(), args){ if(condition){ f() } }
*/

/*namespace L3.Higher_Order_Functions;

class Cache<T> where T : class 
{
    public T Get(Guid id) => throw new NotImplementedException(); // instead it supposed to have some logic to get data from cache
    public T Get(Guid id, Func<T> onMiss)
        => Get(id) ?? onMiss();
}*/

#endregion

#region Adapter functions

/*Some HOFs don’t apply the given function at all, but rather return a new function,
somehow related to the function given as an argument. For example, say you have a
    function that performs integer division:*/

/*Func<double, double, double> divide = (x, y) => x / y;
divide(10, 2); // => 5

Console.WriteLine(divide(10, 2));

var divideReverse = divide.SwapArgs();

Console.WriteLine(divideReverse(10, 2));

public static class FuncExtensions
{
    public static Func<T2, T1, R> SwapArgs<T1, T2, R>(
        this Func<T1, T2, R> f)
        => (t2, t1) => f(t1, t2);
}*/

/* Playing with this sort of HOF leads to the interesting idea that functions aren’t set in
 stone: if you don’t like the interface of a function, you can call it via another function
 that provides an interface that better suits your needs. That’s why I call these adapter
 functions.
 
 The well-known adapter pattern in OOP can be seen as applying the idea of adapter functions
  to an object’s interface.*/

#endregion

#region Functions that create other functions (function factories)

Func<int, bool> IsMod(int n) => i => i % n == 0;
Enumerable.Range(1, 20).Where(IsMod(3)).ToList().ForEach(Console.WriteLine); // created mod 3
Enumerable.Range(1, 20).Where(IsMod(4)).ToList().ForEach(Console.WriteLine); // creates mod 4


#endregion