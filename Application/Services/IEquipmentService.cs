using Domain.Entities;

namespace Application.Services;

public interface IEquipmentService
{
    Task<IEnumerable<Equipment>> GetAllEquipmentsAsync();
    Task<Equipment> GetEquipmentByIdAsync(Guid id);
    Task<Equipment> CreateEquipmentAsync(Equipment equipment);
    Task<Equipment> UpdateEquipmentAsync(Equipment equipment);
    Task DeleteEquipmentAsync(Guid id);
    Task<IEnumerable<Equipment>> GetEquipmentsByBrandAsync(string brand);
    Task<Equipment> GetEquipmentBySerialNumberAsync(string serialNumber);
}
