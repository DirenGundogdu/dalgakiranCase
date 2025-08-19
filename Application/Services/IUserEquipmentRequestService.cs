using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public interface IUserEquipmentRequestService
{
    Task<IEnumerable<UserEquipmentRequest>> GetAllRequestsAsync();
    Task<UserEquipmentRequest> GetRequestByIdAsync(Guid id);
    Task<IEnumerable<UserEquipmentRequest>> GetRequestsByUserIdAsync(Guid userId);
    Task<IEnumerable<UserEquipmentRequest>> GetRequestsByEquipmentIdAsync(Guid equipmentId);
    Task<IEnumerable<UserEquipmentRequest>> GetRequestsByPriorityAsync(Priority priority);
    Task<UserEquipmentRequest> CreateRequestAsync(UserEquipmentRequest request);
    Task<UserEquipmentRequest> UpdateRequestAsync(UserEquipmentRequest request);
    Task DeleteRequestAsync(Guid id);
    Task<IEnumerable<UserEquipmentRequest>> GetPendingRequestsAsync();
}
