using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasKey(ur => ur.Id);
        builder.HasOne(ur => ur.User)
            .WithOne(u => u.UserRole)
            .HasForeignKey<UserRole>(ur => ur.UserId);
        builder.HasOne(ur => ur.Role)
            .WithOne(r => r.UserRole)
            .HasForeignKey<UserRole>(ur => ur.RoleId);
    }
}

