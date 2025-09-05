namespace L7.C2.Exercise.Body_Mass_Index;

public sealed class BodyMassIndexApp
{
    public void Run()
    {
        Write(new Body(Read, Read)).Invoke();
    }
    
    private Func<string, double> Read => s =>
    {
        Console.WriteLine($"Enter your {s}:");
        var res = Console.ReadLine();
        return double.TryParse(res, out var value) ? value : Read(s);
    };
    
    public Func<Body, Action> Write => b => () => 
        DisplayCategory(MassIndexCategory(MassIndex(b.Mass, b.Height)));
    
    public void DisplayCategory(string bmiCategory)
        => Console.WriteLine($"Based on your BMI, you are {bmiCategory}");

    public Func<double,string> MassIndexCategory => d => d switch
    {
        < 18.5 => nameof(BodyCategory.Underweight),
        >= 18.5 and < 25 => nameof(BodyCategory.Healthy),
        >= 25 and < 30 => nameof(BodyCategory.Overweight),
        _ => nameof(BodyCategory.Obesity) // my addition
    };

    /// <summary>
    /// Body Mass Index
    /// </summary>
    public Func<double, double, double> MassIndex => (mass, height) =>
    {
        return Math.Round(mass / Square(height), 2);
        double Square(double d) => Math.Pow(d, 2);
    };
}