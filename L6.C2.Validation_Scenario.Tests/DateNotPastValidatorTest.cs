namespace L6.C2.Validation_Scenario.Tests;

// in book it's a bit different I made my solution, but idea is the same 
public class DateNotPastValidatorTest
{
    static DateTime presentDate = new DateTime(2016, 12, 12);
    private class FakeDateTimeService : IDateTimeService 
    {
        public DateTime UtcNow => presentDate;
    }
    
    [Fact]
    public void WhenTransferDateIsPast_ThenValidationFails()
    {
        var sut = new DateNotPastValidator(new FakeDateTimeService()); 
        var cmd = new MakeTransfer { Date = presentDate.AddDays(-1) };
        Assert.False(sut.IsValid(cmd));
    }
    
    [Fact]
    public void WhenTransferDateIsNotInPast_ThenValidationSucceeds()
    {
        var sut = new DateNotPastValidator(new FakeDateTimeService()); 
        var cmd = new MakeTransfer { Date = presentDate.AddDays(1) };
        Assert.True(sut.IsValid(cmd));
    }
}