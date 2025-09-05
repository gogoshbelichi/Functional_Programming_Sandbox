namespace L6.C2.Validation_Scenario;
                                 // better for tests to make predictable behaviour
public class DateNotPastValidator(IDateTimeService clock) : IValidator<MakeTransfer> 
{
    public bool IsValid(MakeTransfer request)
        => clock.UtcNow.Date <= request.Date.Date;
}

/*Let’s look at the refactored IsValid method: is it a pure function? Well, the answer is,
it depends! It depends, of course, on the implementation of IDateTimeService that’s
injected:
     When running normally, you’ll compose your objects so that you get the “real”
    impure implementation that checks the system clock.
    
     When running unit tests, you’ll inject a “fake” pure implementation that does
    something predictable, such as always returning the same DateTime, enabling
    you to write tests that are repeatable.*/