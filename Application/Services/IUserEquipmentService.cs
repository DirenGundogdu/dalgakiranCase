using Domain.Entities;

namespace Application.Services;

public interface IUserEquipmentService
{
    Task<IEnumerable<UserEquipment>> GetAllUserEquipmentsAsync();
    Task<UserEquipment> GetUserEquipmentByIdAsync(Guid id);
    Task<UserEquipment> GetUserEquipmentsByUserIdAsync(Guid userId);
    Task<IEnumerable<UserEquipment>> GetUserEquipmentsByEquipmentIdAsync(Guid equipmentId);
    Task<UserEquipment> CreateUserEquipmentAsync(UserEquipment userEquipment);
    Task<UserEquipment> UpdateUserEquipmentAsync(UserEquipment userEquipment);
    Task DeleteUserEquipmentAsync(Guid id);
}
