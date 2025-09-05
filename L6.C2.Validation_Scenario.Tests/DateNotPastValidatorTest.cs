namespace L6.C2.Validation_Scenario.Tests;

// in book it's a bit different I made my solution, but idea is the same 
public class DateNotPastValidatorTest
{
    static DateTime presentDate = new DateTime(2016, 12, 12);
    private class FakeDateTimeService : IDateTimeService //header interface
    {
        public DateTime UtcNow => presentDate;
    }
    /*One of the least desirable effects of using this approach systematically is the
    explosion in the number of interfaces, because you must define an interface for every
    component that has an I/O element*/
    
    // this
    [Theory]
    [InlineData(-1, false)] // yesterday -> fail
    [InlineData(0, true)]   // today -> ok
    [InlineData(1, true)]   // tomorrow -> ok
    public void TransferDateValidation(int offsetDays, bool expectedResult)
    {
        var sut = new DateNotPastValidator(new FakeDateTimeService());
        var cmd = new MakeTransfer { Date = presentDate.AddDays(offsetDays) };

        Assert.Equal(expectedResult, sut.IsValid(cmd));
    }
    
    // instead of this
    [Fact]
    public void WhenTransferDateIsPast_ThenValidationFails()
    {
        var sut = new DateNotPastValidator(new FakeDateTimeService()); 
        var cmd = new MakeTransfer { Date = presentDate.AddDays(-1) };
        Assert.False(sut.IsValid(cmd));
    }
    
    // and instead of this
    [Fact]
    public void WhenTransferDateIsNotInPast_ThenValidationSucceeds()
    {
        var sut = new DateNotPastValidator(new FakeDateTimeService()); 
        var cmd = new MakeTransfer { Date = presentDate.AddDays(1) };
        Assert.True(sut.IsValid(cmd));
    }
}

/*                          Why testing impure functions is hard
 
    When you write unit tests, what are you testing? A unit, of course, but what’s a unit
exactly? Whatever unit you’re testing is a function or can be viewed as one.
    If what you’re testing actually is a pure function, testing is easy: you just give it an
    input and verify that the output is as expected, as illustrated in figure 2.3. If you use
the standard Arrange Act Assert (AAA) pattern in your unit tests,6 and the unit you’re
testing is a pure function, then the arrange step consists of defining the input values,
    the act step is the function invocation, and the assert step consists of checking that the
 output is as expected.
 
 figure 2.3 Testing a pure function is easy: AAA pattern
 arrange: setup               act: evaluate          assert: verify output
 inputs --------------------> unit under test -------------------> outputs  
 
 impure logic examples:
      The date validator depends on the state of the world, specifically the current
     time.
     
      A void-returning method that sends an email has no explicit output to assert
     against, but it results in a new state of the world.
     
      A method that sets a non-local variable results in a new state of the program.
 */