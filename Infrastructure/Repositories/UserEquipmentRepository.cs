using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserEquipmentRepository : Repository<UserEquipment>, IUserEquipmentRepository
{
    public UserEquipmentRepository(AppDbContext context) : base(context) { }
    public async Task<UserEquipment> GetUserEquipmentByUserIdAsync(Guid userId) {
        return await _context.UserEquipments
            .Include(x=>x.Equipment).FirstOrDefaultAsync(ue => ue.UserId == userId);
    }
}

