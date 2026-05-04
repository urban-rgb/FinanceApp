namespace FinanceApp.Models;

public class FinanceSettings
{
    public const string SectionName = "FinanceSettings";

    public decimal MaxTransactionAmount { get; set; }
    
    public string DefaultCurrency { get; set; } = "USD";
}