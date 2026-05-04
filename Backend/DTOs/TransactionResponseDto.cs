using FinanceApp.Models;

namespace FinanceApp.DTOs;

public class TransactionResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}