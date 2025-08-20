using Domain.Entities;

namespace Application.Services;

public interface IUserService
{
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
}
