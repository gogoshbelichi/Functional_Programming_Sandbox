namespace L1.Introduction;

public class Circle
{
    public Circle(double radius) => Radius = radius;
    public double Radius { get; }
    public double Circumference => 2 * Math.PI * Radius; // static func-way to introduce a variable

    public double Area
    {
        get
        {
            double Square(double d) => Math.Pow(d, 2);  // immutable auto-property
            return Math.PI * Square(Radius);
        }
    }
    
    public (double Circumference,  double Area) Stats => (Circumference, Area); // tuple syntax
}