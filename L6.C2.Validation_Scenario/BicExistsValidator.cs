namespace L6.C2.Validation_Scenario;

// BicFormatValidator.cs idea development
public class BicExistsValidator(Func<IEnumerable<string>> getValidCodes) : IValidator<MakeTransfer>
{
    public bool IsValid(MakeTransfer cmd)
        => getValidCodes().Contains(cmd.Bic);
}