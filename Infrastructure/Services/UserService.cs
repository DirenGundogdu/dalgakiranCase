using Application.Services;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetById((int)id.GetHashCode());
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
       return await _userRepository.GetUserWithRolesAsync(email);
        
    }

    public async Task<User> CreateUserAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Email))
            throw new ArgumentException("Email is required");

        var existingUser = await GetUserByEmailAsync(user.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists");

        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _userRepository.Create(user);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.Update(user);
        return user;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        if (user != null)
        {
            await _userRepository.Delete(user);
        }
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = await GetUserByEmailAsync(email);
        return user != null && user.Password == password;
    }
}
