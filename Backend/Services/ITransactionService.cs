using FinanceApp.DTOs;

namespace FinanceApp.Services;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateTransactionAsync(TransactionCreateDto dto);
    Task<List<TransactionResponseDto>> GetUserTransactionsAsync(Guid userId);
    Task<TransactionResponseDto?> GetTransactionByIdAsync(Guid id);
    Task<bool> DeleteTransactionAsync(Guid id); 
    Task<TransactionResponseDto?> UpdateTransactionAsync(Guid id, TransactionUpdateDto dto); 
    
}