using Application.DTOs;
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
        return await _requestRepository.GetAllRequestsAsync();
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

    public async Task<UserEquipmentRequest> CreateRequestAsync(CreateUserEquipmentRequestDTO request)
    {
        var user = await _userRepository.GetUserByGuidIdAsync(request.UserId);
        if (user == null)
            throw new ArgumentException("User not found");

        var equipments = await _equipmentRepository.GetAllUnassignedEquipment();
        
       var equipment = equipments.FirstOrDefault(x=> x.Id == request.EquipmentId);
       
        if (equipment == null)
            throw new ArgumentException("Equipment not found");
        
        var newRequest = new UserEquipmentRequest {
            Id = Guid.CreateVersion7(),
            UserId = request.UserId,
            EquipmentId = request.EquipmentId,
            Priority = (Priority)request.Priority,
            Status = EquipmentStatus.pending,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        await _requestRepository.Create(newRequest);
        return newRequest;
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

    public async Task UpdateRequestStatusAsync(Guid requestId, int status, Guid userId) {
        await _requestRepository.UpdateRequestStatus(requestId,status,userId);
    }
}
