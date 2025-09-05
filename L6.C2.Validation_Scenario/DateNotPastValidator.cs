namespace L6.C2.Validation_Scenario;
                                 // better for tests to make predictable behaviour
public class DateNotPastValidator(IDateTimeService clock /*DateTime today*/) : IValidator<MakeTransfer> 
{
    public bool IsValid(MakeTransfer request)
        => clock.UtcNow.Date /*today*/ <= request.Date.Date;
    /*
        Instead of injecting an interface, exposing some method you can invoke, inject a
        value. Now the implementation of IsValid is pure (because today is not mutable)
        
        But I left first impl cause of laziness to make changes in a couple of lines, sorry :(
    */
}

/*Let’s look at the refactored IsValid method: is it a pure function? Well, the answer is,
it depends! It depends, of course, on the implementation of IDateTimeService that’s
injected:
     When running normally, you’ll compose your objects so that you get the “real”
    impure implementation that checks the system clock.
    
     When running unit tests, you’ll inject a “fake” pure implementation that does
    something predictable, such as always returning the same DateTime, enabling
    you to write tests that are repeatable.*/