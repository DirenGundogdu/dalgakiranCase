using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserEquipmentConfiguration : IEntityTypeConfiguration<UserEquipment>
{
    public void Configure(EntityTypeBuilder<UserEquipment> builder)
    {
        builder.ToTable("UserEquipments");
        builder.HasKey(ue => ue.Id);
        builder.HasOne(ue => ue.User)
            .WithMany(u => u.UserEquipments)
            .HasForeignKey(ue => ue.UserId);
        builder.HasOne(ue => ue.Equipment)
            .WithMany(e => e.UserEquipments)
            .HasForeignKey(ue => ue.EquipmentId);
    }
}

