using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(AppDbContext context) : base(context) { }
}

