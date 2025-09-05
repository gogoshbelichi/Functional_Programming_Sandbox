using L6.C2.Validation_Scenario;

public sealed class MakeTransfer : Command
{
    public Guid DebitedAccountId { get; set; }
    public string Beneficiary { get; set; }
    public string Iban { get; set; }
    public string Bic { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}