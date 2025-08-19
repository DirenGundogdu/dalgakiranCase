namespace Application.Interfaces;

using Domain.Entities;

public interface IUserEquipmentRepository : IRepository<UserEquipment>
{
  Task<UserEquipment> GetUserEquipmentByUserIdAsync(Guid userId);
}

