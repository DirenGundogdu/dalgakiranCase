using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class UserEquipmentRequestRepository : Repository<UserEquipmentRequest>, IUserEquipmentRequestRepository
{
    public UserEquipmentRequestRepository(AppDbContext context) : base(context) { }
}

