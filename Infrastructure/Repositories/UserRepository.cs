using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
    public async Task<User> GetUserByEmailAsync(string email) {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetUserByGuidIdAsync(Guid Id) {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
    }
}

