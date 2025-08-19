namespace Application.Interfaces;

public interface IUserRepository : IRepository<Domain.Entities.User>
{
    //get user With roles
    Task<Domain.Entities.User> GetUserWithRolesAsync(string Email);
}