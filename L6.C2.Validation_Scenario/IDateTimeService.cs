namespace L6.C2.Validation_Scenario;
// Bringing impure functions under test
public interface IDateTimeService
{
    DateTime UtcNow { get; } 
}