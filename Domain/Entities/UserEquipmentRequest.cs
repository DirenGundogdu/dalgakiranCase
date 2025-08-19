using Domain.Enums;

namespace Domain.Entities;

public sealed class UserEquipmentRequest : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public User User { get; set; }
    public Equipment Equipment { get; set; }
    
}