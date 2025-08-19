using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services;

public class UserEquipmentRequestService : IUserEquipmentRequestService
{
    private readonly IUserEquipmentRequestRepository _requestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEquipmentRepository _equipmentRepository;

    public UserEquipmentRequestService(
        IUserEquipmentRequestRepository requestRepository, 
        IUserRepository userRepository, 
        IEquipmentRepository equipmentRepository)
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<UserEquipmentRequest>> GetAllRequestsAsync()
    {
        return await _requestRepository.GetAll();
    }

    public async Task<UserEquipmentRequest> GetRequestByIdAsync(Guid id)
    {
        return await _requestRepository.GetById((int)id.GetHashCode());
    }

    public async Task<IEnumerable<UserEquipmentRequest>> GetRequestsByUserIdAsync(Guid userId)
    {
        var requests = await _requestRepository.GetAll();
        return requests.Where(r => r.UserId == userId);
    }

    public async Task<IEnumerable<UserEquipmentRequest>> GetRequestsByEquipmentIdAsync(Guid equipmentId)
    {
        var requests = await _requestRepository.GetAll();
        return requests.Where(r => r.EquipmentId == equipmentId);
    }

    public async Task<IEnumerable<UserEquipmentRequest>> GetRequestsByPriorityAsync(Priority priority)
    {
        var requests = await _requestRepository.GetAll();
        return requests.Where(r => r.Priority == priority);
    }

    public async Task<UserEquipmentRequest> CreateRequestAsync(UserEquipmentRequest request)
    {
        if (string.IsNullOrEmpty(request.Description))
            throw new ArgumentException("Request description is required");

        // Validate user exists
        var user = await _userRepository.GetById((int)request.UserId.GetHashCode());
        if (user == null)
            throw new ArgumentException("User not found");

        // Validate equipment exists
        var equipment = await _equipmentRepository.GetById((int)request.EquipmentId.GetHashCode());
        if (equipment == null)
            throw new ArgumentException("Equipment not found");

        request.Id = Guid.NewGuid();
        request.CreatedAt = DateTime.UtcNow;
        request.UpdatedAt = DateTime.UtcNow;

        await _requestRepository.Create(request);
        return request;
    }

    public async Task<UserEquipmentRequest> UpdateRequestAsync(UserEquipmentRequest request)
    {
        request.UpdatedAt = DateTime.UtcNow;
        await _requestRepository.Update(request);
        return request;
    }

    public async Task DeleteRequestAsync(Guid id)
    {
        var request = await GetRequestByIdAsync(id);
        if (request != null)
        {
            await _requestRepository.Delete(request);
        }
    }

    public async Task<IEnumerable<UserEquipmentRequest>> GetPendingRequestsAsync()
    {
        var requests = await _requestRepository.GetAll();
        return requests.OrderByDescending(r => r.Priority)
                      .ThenBy(r => r.CreatedAt);
    }
}
