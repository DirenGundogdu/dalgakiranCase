using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserEquipmentRequestConfiguration : IEntityTypeConfiguration<UserEquipmentRequest>
{
    public void Configure(EntityTypeBuilder<UserEquipmentRequest> builder)
    {
        builder.ToTable("UserEquipmentRequests");
        builder.HasKey(uer => uer.Id);
        builder.Property(uer => uer.Description).IsRequired().HasMaxLength(256);
        builder.Property(uer => uer.Priority).IsRequired();
        builder.HasOne(uer => uer.User)
            .WithMany(u => u.UserEquipmentRequests)
            .HasForeignKey(uer => uer.UserId);
        builder.HasOne(uer => uer.Equipment)
            .WithMany(e => e.UserEquipmentRequests)
            .HasForeignKey(uer => uer.EquipmentId);
    }
}

