namespace L7.C2.Exercise.Body_Mass_Index;

/// <summary>
/// Body data
/// </summary>
/// <param name="mass">Mass in kg</param>
/// <param name="height">Height in cm</param>
public sealed class Body(double mass, double height)
{
    /// <summary>
    /// Mass in kg
    /// </summary>
    internal double Mass { get; } = mass;

    /// <summary>
    /// Height in m
    /// </summary>
    internal double Height { get; } = height;
}