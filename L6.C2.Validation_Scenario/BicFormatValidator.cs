using System.Text.RegularExpressions;

namespace L6.C2.Validation_Scenario;

                                       //provider
public sealed class BicFormatValidator(/*IValidCodesProvider p*/) : IValidator<MakeTransfer> 
{
    static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$"); // usually banks have smth like this
                                                                      // readonly IEnumerable<string> validCodes = p.GetCodes(); 
                                                                      // which you get from some provider (my idea)
                                                                      // BUT book approach is in BicExistsValidator.cs
    public bool IsValid(MakeTransfer cmd) => regex.IsMatch(cmd.Bic);  // and  => validCodes.Contains(cmd.Bic);
}