using FinanceApp.Models;

namespace FinanceApp.DTOs;

public class TransactionCreateDto
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Guid UserId { get; set; }
}