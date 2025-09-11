namespace L14.C4.Exercises;
// Task model
public class Employee
{
    public string Id { get; set; }
    public Option<WorkPermit> WorkPermit { get; set; }
    // public set for tests
    public DateTime JoinedOn { get; set; }
    // public set for tests
    public Option<DateTime> LeftOn { get; set; }
}