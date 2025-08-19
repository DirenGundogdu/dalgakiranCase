using Domain.Entities;

namespace Application.Interfaces;

public interface IUserEquipmentService
{
    Task<IEnumerable<UserEquipment>> GetAllUserEquipmentsAsync();
    Task<UserEquipment> GetUserEquipmentByIdAsync(Guid id);
    Task<IEnumerable<UserEquipment>> GetUserEquipmentsByUserIdAsync(Guid userId);
    Task<IEnumerable<UserEquipment>> GetUserEquipmentsByEquipmentIdAsync(Guid equipmentId);
    Task<UserEquipment> CreateUserEquipmentAsync(UserEquipment userEquipment);
    Task<UserEquipment> UpdateUserEquipmentAsync(UserEquipment userEquipment);
    Task DeleteUserEquipmentAsync(Guid id);
    Task AssignEquipmentToUserAsync(Guid userId, Guid equipmentId);
    Task UnassignEquipmentFromUserAsync(Guid userId, Guid equipmentId);
}
