using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(AppDbContext context) : base(context) { }


    
    public async Task<IEnumerable<Equipment>> GetAllUnassignedEquipment() {
        return await _context.Equipments.Where(x => !x.UserEquipments.Any()).ToListAsync();
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentWithUserAsync() {
        return await _context.Equipments.Include(x => x.UserEquipments).ThenInclude(x => x.User).ToListAsync();
    }


}

