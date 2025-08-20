using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserEquipmentRequestRepository : Repository<UserEquipmentRequest>, IUserEquipmentRequestRepository
{
    public UserEquipmentRequestRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<UserEquipmentRequest>> GetAllRequestsAsync() {
        return await _context.UserEquipmentRequests.Include(x => x.User).Include(x => x.Equipment).ToListAsync();
    }

    public async Task UpdateRequestStatus(Guid requestId, int status, Guid userId) {
        var request = await _context.UserEquipmentRequests.FirstOrDefaultAsync(x => x.Id == requestId);
        if (request != null) {
            request.Status = (EquipmentStatus)status;
            _context.UserEquipmentRequests.Update(request);

            if (status == 2)
            {
                var userEquipments = new UserEquipment {
                    UserId = userId,
                    EquipmentId = request.EquipmentId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.UserEquipments.Add(userEquipments);
            }
            
            
            await _context.SaveChangesAsync();
        }
    }
}

