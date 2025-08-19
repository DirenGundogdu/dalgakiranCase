namespace Domain.Entities;

public sealed class Role : BaseEntity
{
    public string Name { get; set; }
    public UserRole UserRole { get; set; }
}