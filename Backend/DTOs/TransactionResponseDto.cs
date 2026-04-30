using FinanceApp.Models;

namespace FinanceApp.DTOs;

public class TransactionResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}