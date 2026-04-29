using FinanceApp.DTOs;

namespace FinanceApp.Services;

public interface IUserService
{
    Task<UserResponseDto?> GetUserByIdAsync(Guid id);
}