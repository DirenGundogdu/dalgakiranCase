using Application.Services;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserEquipmentService : IUserEquipmentService
{
    private readonly IUserEquipmentRepository _userEquipmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEquipmentRepository _equipmentRepository;

    public UserEquipmentService(
        IUserEquipmentRepository userEquipmentRepository, 
        IUserRepository userRepository, 
        IEquipmentRepository equipmentRepository)
    {
        _userEquipmentRepository = userEquipmentRepository;
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<UserEquipment>> GetAllUserEquipmentsAsync()
    {
        return await _userEquipmentRepository.GetAll();
    }

    public async Task<UserEquipment> GetUserEquipmentByIdAsync(Guid id)
    {
        return await _userEquipmentRepository.GetById((int)id.GetHashCode());
    }

    public async Task<IEnumerable<UserEquipment>> GetUserEquipmentsByUserIdAsync(Guid userId)
    {
        return await _userEquipmentRepository.GetUserEquipmentByUserIdAsync(userId);
       
    }

    public async Task<IEnumerable<UserEquipment>> GetUserEquipmentsByEquipmentIdAsync(Guid equipmentId)
    {
        var userEquipments = await _userEquipmentRepository.GetAll();
        return userEquipments.Where(ue => ue.EquipmentId == equipmentId);
    }

    public async Task<UserEquipment> CreateUserEquipmentAsync(UserEquipment userEquipment)
    {
        // Validate user exists
        var user = await _userRepository.GetById((int)userEquipment.UserId.GetHashCode());
        if (user == null)
            throw new ArgumentException("User not found");

        // Validate equipment exists
        var equipment = await _equipmentRepository.GetById((int)userEquipment.EquipmentId.GetHashCode());
        if (equipment == null)
            throw new ArgumentException("Equipment not found");

        userEquipment.Id = Guid.NewGuid();
        userEquipment.CreatedAt = DateTime.UtcNow;
        userEquipment.UpdatedAt = DateTime.UtcNow;

        await _userEquipmentRepository.Create(userEquipment);
        return userEquipment;
    }

    public async Task<UserEquipment> UpdateUserEquipmentAsync(UserEquipment userEquipment)
    {
        userEquipment.UpdatedAt = DateTime.UtcNow;
        await _userEquipmentRepository.Update(userEquipment);
        return userEquipment;
    }

    public async Task DeleteUserEquipmentAsync(Guid id)
    {
        var userEquipment = await GetUserEquipmentByIdAsync(id);
        if (userEquipment != null)
        {
            await _userEquipmentRepository.Delete(userEquipment);
        }
    }
    
}
