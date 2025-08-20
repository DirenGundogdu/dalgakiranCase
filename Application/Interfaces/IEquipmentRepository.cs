namespace Application.Interfaces;

using Domain.Entities;

public interface IEquipmentRepository : IRepository<Equipment>
{
    Task<IEnumerable<Equipment>> GetAllUnassignedEquipment();
    
    Task<IEnumerable<Equipment>> GetAllEquipmentWithUserAsync();
}

