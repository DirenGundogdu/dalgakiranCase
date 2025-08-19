using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("Equipments");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Brand).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Model).IsRequired().HasMaxLength(50);
        builder.Property(e => e.SerialNumber).IsRequired().HasMaxLength(100);
        builder.HasMany(e => e.UserEquipments)
            .WithOne(ue => ue.Equipment)
            .HasForeignKey(ue => ue.EquipmentId);
        builder.HasMany(e => e.UserEquipmentRequests)
            .WithOne(uer => uer.Equipment)
            .HasForeignKey(uer => uer.EquipmentId);
    }
}

