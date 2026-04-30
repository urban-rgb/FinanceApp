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
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (dto.Type == TransactionType.Income)
        {
            user.Balance += dto.Amount;
        }
        else
        {
            user.Balance -= dto.Amount;
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

    public async Task<List<TransactionResponseDto>> GetUserTransactionsAsync(Guid userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                Name = t.Name,
                Amount = t.Amount,
                Type = t.Type,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
    }
    
    public async Task<TransactionResponseDto?> GetTransactionByIdAsync(Guid id)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null) return null;

        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Name = transaction.Name,
            Amount = transaction.Amount,
            Type = transaction.Type,
            CreatedAt = transaction.CreatedAt
        };
    }
    
    public async Task<bool> DeleteTransactionAsync(Guid id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null) return false;

        if (transaction.Type == TransactionType.Income)
        {
            transaction.User.Balance -= transaction.Amount;
        }
        else
        {
            transaction.User.Balance += transaction.Amount;
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<TransactionResponseDto?> UpdateTransactionAsync(Guid id, TransactionUpdateDto dto)
    {
        var transaction = await _context.Transactions
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null) return null;

        if (transaction.Type == TransactionType.Income)
        {
            transaction.User.Balance -= transaction.Amount;
        }
        else
        {
            transaction.User.Balance += transaction.Amount;
        }

        transaction.Name = dto.Name;
        transaction.Amount = dto.Amount;
        transaction.Type = dto.Type;

        if (transaction.Type == TransactionType.Income)
        {
            transaction.User.Balance += transaction.Amount;
        }
        else
        {
            transaction.User.Balance -= transaction.Amount;
        }

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