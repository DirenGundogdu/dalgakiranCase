using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserEquipmentRepository : Repository<UserEquipment>, IUserEquipmentRepository
{
    public UserEquipmentRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<UserEquipment>> GetUserEquipmentByUserIdAsync(Guid userId) {
        return await _context.UserEquipments.Where(x => x.UserId == userId)
            .Include(x=>x.Equipment).ToListAsync();
    }
}

