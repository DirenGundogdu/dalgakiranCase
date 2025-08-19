using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{

    public void Configure(EntityTypeBuilder<User> builder) {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasOne(u => u.UserRole)
            .WithOne(x => x.User)
            .HasForeignKey<UserRole>(x => x.UserId);

        builder.HasMany(u => u.UserEquipments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        builder.HasMany(u => u.UserEquipmentRequests)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}