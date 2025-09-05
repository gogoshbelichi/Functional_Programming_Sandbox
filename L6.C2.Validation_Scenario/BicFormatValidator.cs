using System.Text.RegularExpressions;

namespace L6.C2.Validation_Scenario;

public sealed class BicFormatValidator : IValidator<MakeTransfer> 
{
    static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$"); //
    public bool IsValid(MakeTransfer cmd) => regex.IsMatch(cmd.Bic);
}