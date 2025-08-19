namespace Domain.Entities;

public sealed class Equipment : BaseEntity
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public List<UserEquipment> UserEquipments { get; set; }
    public List<UserEquipmentRequest> UserEquipmentRequests { get; set; }
}