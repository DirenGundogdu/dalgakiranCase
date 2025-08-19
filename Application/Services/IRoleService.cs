using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(Guid id);
    Task<Role> GetRoleByNameAsync(string name);
    Task<Role> CreateRoleAsync(Role role);
    Task<Role> UpdateRoleAsync(Role role);
    Task DeleteRoleAsync(Guid id);
}
