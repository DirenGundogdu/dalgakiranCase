namespace Application.DTOs;

public class CreateUserEquipmentRequestDTO
{
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    
}