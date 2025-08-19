using Domain.Entities;

namespace Application.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
