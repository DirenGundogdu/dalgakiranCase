using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAll();
    }

    public async Task<Role> GetRoleByIdAsync(Guid id)
    {
        return await _roleRepository.GetById((int)id.GetHashCode());
    }

    public async Task<Role> GetRoleByNameAsync(string name)
    {
        var roles = await _roleRepository.GetAll();
        return roles.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());
    }

    public async Task<Role> CreateRoleAsync(Role role)
    {
        if (string.IsNullOrEmpty(role.Name))
            throw new ArgumentException("Role name is required");

        var existingRole = await GetRoleByNameAsync(role.Name);
        if (existingRole != null)
            throw new InvalidOperationException("Role with this name already exists");

        role.Id = Guid.NewGuid();
        role.CreatedAt = DateTime.UtcNow;
        role.UpdatedAt = DateTime.UtcNow;

        await _roleRepository.Create(role);
        return role;
    }

    public async Task<Role> UpdateRoleAsync(Role role)
    {
        role.UpdatedAt = DateTime.UtcNow;
        await _roleRepository.Update(role);
        return role;
    }

    public async Task DeleteRoleAsync(Guid id)
    {
        var role = await GetRoleByIdAsync(id);
        if (role != null)
        {
            await _roleRepository.Delete(role);
        }
    }
}
