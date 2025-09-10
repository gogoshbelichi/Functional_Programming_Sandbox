using Option;

public class Age
{ 
    private int Value { get; } 
    /*The internal representation
    is kept private.*/
    
    public static Option<Age> Of(int age)
        => IsValid(age) ? new Some<Age>(new Age(age)) : None.Default;
    
    public static bool operator <(Age l, Age r) 
        => l.Value < r.Value;
    public static bool operator >(Age l, Age r)
        => l.Value > r.Value;
    public static bool operator <(Age l, int r) 
        => l < new Age(r);
    public static bool operator >(Age l, int r)
        => l > new Age(r);
    
    // dishonest constructor
    public Age(int value)
    {
        if (!IsValid(value))
            throw new ArgumentException($"{value} is not a valid age");
        Value = value;
    }
    private static bool IsValid(int age)
        => 
            0 <= age && age < 120;
    
    // this makes easier to log in console :)
    public static implicit operator string(Age a)
        => a.Value.ToString();
    
    public int GetValue() => Value;
}