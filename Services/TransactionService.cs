using Microsoft.EntityFrameworkCore;
using FinanceApp.Data;
using FinanceApp.DTOs;
using FinanceApp.Models;

namespace FinanceApp.Services;

public class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionResponseDto> CreateTransactionAsync(TransactionCreateDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists)
        {
            throw new Exception("User not found");
        }
        
        // todo add throw exceptions everywhere
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Amount = dto.Amount,
            Type = dto.Type,
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Name = transaction.Name,
            Amount = transaction.Amount,
            Type = transaction.Type,
            CreatedAt = transaction.CreatedAt
        };
    }
}