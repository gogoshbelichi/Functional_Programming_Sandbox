//  Pure functions                                              Impure functions
// The output depends entirely on the input arguments.         Factors other than input arguments may affect the output.
// Cause no side effects.                                      May cause side effects

/*To clarify this definition, we must define exactly what a side effect is. A function is said
    to have side effects if it does any of the following:
     Mutates global state—“Global” here means any state that’s visible outside of the
    function’s scope. For example, a private instance field is considered global
because it’s visible from all methods within the class.
     Mutates its input arguments
     Throws exceptions
     Performs any I/O operation—This includes any interaction between the program and
the external world, including reading from or writing to the console, the filesystem,
or a database, and interacting with any process outside the application’s boundary.*/

/* Some will argue that a function can be considered pure despite throwing exceptions. However, in throwing
 exceptions it will cause indeterminism to appear in code that makes some decisions based on exception
handling, or in the absence of exception handling, in the side effect of the program crashing.

The parts of your program that consist entirely of pure functions can be optimized
 in a number of ways:
     Parallelization — Different threads carry out tasks in parallel
     Lazy evaluation — Only evaluate values as needed
     Memoization — Cache the result of a function so it’s only computed once
 On the other hand, using these techniques with impure functions can lead to rather
 nasty bugs. For these reasons, FP advocates that pure functions should be preferred
 whenever possible.  */
 
 /* Strategies for managing side effects:
      ISOLATE I/O EFFECTS 
        Example: Read Input File (not pure) => Convert format (pure) => Write converted file (not pure)
      AVOID MUTATING ARGUMENTS
        BAD: decimal RecomputeTotal(Order order, List<OrderLine> linesToDelete)
             {
                 var result = 0m;
                 foreach (var line in order.OrderLines)
                 if (line.Quantity == 0) linesToDelete.Add(line);
                 else result += line.Product.Price * line.Quantity;
                 return result;
             }
        BETTER:  (decimal, IEnumerable<OrderLine>) RecomputeTotal(Order order)
                      => (order.OrderLines.Sum(l => l.Product.Price * l.Quantity)
                        , order.OrderLines.Where(l => l.Quantity == 0));
*/



(decimal, IEnumerable<OrderLine>) RecomputeTotal(Order order)
     => (order.OrderLines.Sum(l => l.Product.Price * l.Quantity)
         , order.OrderLines.Where(l => l.Quantity == 0));

public class Order(IEnumerable<OrderLine> orderLines)
{
    public IEnumerable<OrderLine> OrderLines { get; } = orderLines;
}

public class OrderLine(Product product, int quantity)
{
    public Product Product { get; set; } = product;
    public int Quantity { get; set; } = quantity;
}

public class Product(decimal price)
{
    public decimal Price { get; set; } = price;
}