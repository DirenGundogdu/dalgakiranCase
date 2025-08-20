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
    
    public async Task<User> GetUserByEmailAsync(string email)
    {
       return await _userRepository.GetUserByEmailAsync(email);
        
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password) {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user != null && user.Password == password;
    }
}
