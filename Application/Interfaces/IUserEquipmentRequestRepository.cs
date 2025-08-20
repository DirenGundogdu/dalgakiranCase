namespace Application.Interfaces;

using Domain.Entities;

public interface IUserEquipmentRequestRepository : IRepository<UserEquipmentRequest>
{
    Task<IEnumerable<UserEquipmentRequest>> GetAllRequestsAsync();

    Task UpdateRequestStatus(Guid requestId, int status, Guid userId);
}

