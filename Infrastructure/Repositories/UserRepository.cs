using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
    public async Task<User> GetUserWithRolesAsync(string email) {
        return await _context.Users
            .Include(u => u.UserRole).ThenInclude(x=> x.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}

