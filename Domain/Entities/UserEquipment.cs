namespace Domain.Entities;

public sealed class UserEquipment : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public User User { get; set; }
    public Equipment Equipment { get; set; }
}