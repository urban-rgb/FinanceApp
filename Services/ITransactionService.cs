using FinanceApp.DTOs;

namespace FinanceApp.Services;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateTransactionAsync(TransactionCreateDto dto);
}