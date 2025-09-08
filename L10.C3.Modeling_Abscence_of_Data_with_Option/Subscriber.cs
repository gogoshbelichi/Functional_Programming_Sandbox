namespace Option;

public class Subscriber
{
    public Option<string> Name { get; set; }
    public string Email { get; set; }
    
    public string GreetingFor(Subscriber subscriber)
        => subscriber.Name.Match(
            () => "Dear Subscriber,",
            (name) => $"Dear {name.ToUpper()},");
}