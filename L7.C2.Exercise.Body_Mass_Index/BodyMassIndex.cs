namespace L7.C2.Exercise.Body_Mass_Index;

public sealed class BodyMassIndexApp
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine(
                Write(
                    new Body(
                        Read(nameof(Body.Mass).ToLower()), 
                        Read(nameof(Body.Height).ToLower())
                    )
                )
                ? "Succeed!"
                : "Failed!");
            Console.WriteLine("Press any key to continue or press ESC to exit...");
            if (Console.ReadKey(intercept: true).Key is not ConsoleKey.Escape) continue;
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }
    }

    private Func<string, double> Read => s =>
    {
        Console.WriteLine($"Enter your {s}:");
        var res = Console.ReadLine();
        return double.TryParse(res, out var value) ? value : Read(s);
    };

    public Func<Body, bool> Write => b => 
    {
        // exclusion of a possible fail, for setting up retries or other exception handling
        try
        {
            Console.WriteLine($"Based on your BMI, you are {MassIndexCategory(MassIndex(b.Mass, b.Height))}");
            return true;
        }
        catch (Exception)
        {
            Console.WriteLine($"Failed to write BMI Category.");
            return false;
        }
    };

    public Func<double, string> MassIndexCategory => d => d switch
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