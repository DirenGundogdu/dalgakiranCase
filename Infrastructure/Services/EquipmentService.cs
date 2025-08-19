using Application.Services;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentsAsync()
    {
        return await _equipmentRepository.GetAll();
    }

    public async Task<Equipment> GetEquipmentByIdAsync(Guid id)
    {
        return await _equipmentRepository.GetById((int)id.GetHashCode());
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment)
    {
        if (string.IsNullOrEmpty(equipment.Name))
            throw new ArgumentException("Equipment name is required");

        if (string.IsNullOrEmpty(equipment.SerialNumber))
            throw new ArgumentException("Serial number is required");

        var existingEquipment = await GetEquipmentBySerialNumberAsync(equipment.SerialNumber);
        if (existingEquipment != null)
            throw new InvalidOperationException("Equipment with this serial number already exists");

        equipment.Id = Guid.NewGuid();
        equipment.CreatedAt = DateTime.UtcNow;
        equipment.UpdatedAt = DateTime.UtcNow;

        await _equipmentRepository.Create(equipment);
        return equipment;
    }

    public async Task<Equipment> UpdateEquipmentAsync(Equipment equipment)
    {
        equipment.UpdatedAt = DateTime.UtcNow;
        await _equipmentRepository.Update(equipment);
        return equipment;
    }

    public async Task DeleteEquipmentAsync(Guid id)
    {
        var equipment = await GetEquipmentByIdAsync(id);
        if (equipment != null)
        {
            await _equipmentRepository.Delete(equipment);
        }
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByBrandAsync(string brand)
    {
        var equipments = await _equipmentRepository.GetAll();
        return equipments.Where(e => e.Brand.ToLower().Contains(brand.ToLower()));
    }

    public async Task<Equipment> GetEquipmentBySerialNumberAsync(string serialNumber)
    {
        var equipments = await _equipmentRepository.GetAll();
        return equipments.FirstOrDefault(e => e.SerialNumber == serialNumber);
    }
}
