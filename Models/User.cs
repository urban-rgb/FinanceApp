namespace FinanceApp.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
}