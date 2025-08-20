namespace Application.Interfaces;

using Domain.Entities;

public interface IUserEquipmentRepository : IRepository<UserEquipment>
{
  Task<IEnumerable<UserEquipment>> GetUserEquipmentByUserIdAsync(Guid userId);
}

