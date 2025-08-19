using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context) { }
}

