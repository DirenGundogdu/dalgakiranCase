namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Enums.Role Role { get; set; }

    public List<UserEquipment> UserEquipments { get; set; }
    public List<UserEquipmentRequest> UserEquipmentRequests { get; set; }
    
}