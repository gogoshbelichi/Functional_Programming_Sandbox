namespace L7.C2.Exercise.Body_Mass_Index;

/// <summary>
/// Body data
/// </summary>
/// <param name="mass">Mass in kg</param>
/// <param name="height">Height in cm</param>
public sealed class Body(Func<string, double> getMass, Func<string, double> getHeight)
{
    /// <summary>
    /// Mass in kg
    /// </summary>
    internal double Mass { get; } = getMass(nameof(Mass).ToLower());

    /// <summary>
    /// Height in m
    /// </summary>
    internal double Height { get; } = getHeight(nameof(Height).ToLower());
}