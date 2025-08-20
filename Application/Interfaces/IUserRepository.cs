namespace Application.Interfaces;

public interface IUserRepository : IRepository<Domain.Entities.User>
{
    Task<Domain.Entities.User> GetUserByEmailAsync(string Email);
    Task<Domain.Entities.User> GetUserByGuidIdAsync(Guid Id);
}