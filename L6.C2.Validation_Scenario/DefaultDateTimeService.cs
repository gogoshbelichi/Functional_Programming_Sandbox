namespace L6.C2.Validation_Scenario;
// Provides a default implementation
public class DefaultDateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow; 
}