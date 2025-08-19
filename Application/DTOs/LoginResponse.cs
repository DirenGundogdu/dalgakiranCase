namespace Application.DTOs;

public class LoginResponse
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string Role { get; set; }
}
