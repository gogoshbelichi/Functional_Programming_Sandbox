namespace L6.C2.Validation_Scenario.Tests;

public class BicExistsValidatorTest
{
    static string[] validCodes = { "ABCDEFGJ123" };
                                                                            
    [Theory]                                                                                 
    [InlineData("ABCDEFGJ123", true)]
    [InlineData("XXXXXXXXXXX", false)]
    public void WhenBicNotFound_ThenValidationFails(string bic, bool expectedResult)
    {
        var validator = new BicExistsValidator(() => validCodes); /*Injects a function that
                                                                                deterministically returns
                                                                                a hard-coded value*/
        var cmd = new MakeTransfer() { Bic = bic };                        
        Assert.Equal(expectedResult, validator.IsValid(cmd));
    }
}