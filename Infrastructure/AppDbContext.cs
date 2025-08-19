using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEquipment> UserEquipments { get; set; }
    public DbSet<UserEquipmentRequest> UserEquipmentRequests { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Sabit GUID'ler tanımla
        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var userRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        
        var user1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var user2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var user3Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var user4Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var user5Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
        
        var laptop1Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
        var monitor1Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var keyboard1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var mouse1Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var printer1Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        // Sabit DateTime değeri
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed Roles - 2 records
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = "Admin", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Role { Id = userRoleId, Name = "Kullanıcı", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // Seed Equipments - 5 records (Türkçe IT ekipmanları)
        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = laptop1Id, Name = "Dizüstü Bilgisayar", Brand = "Dell", Model = "Latitude 7420", SerialNumber = "DL001", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = monitor1Id, Name = "Monitör", Brand = "Samsung", Model = "27UC850", SerialNumber = "SM002", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = keyboard1Id, Name = "Klavye", Brand = "Logitech", Model = "MX Keys", SerialNumber = "LG003", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = mouse1Id, Name = "Fare", Brand = "Logitech", Model = "MX Master 3", SerialNumber = "LG004", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = printer1Id, Name = "Yazıcı", Brand = "HP", Model = "LaserJet Pro", SerialNumber = "HP005", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // Seed Users - 5 records
        modelBuilder.Entity<User>().HasData(
            new User { Id = user1Id, FirstName = "Ali", LastName = "Veli", Email = "ali.veli@dalgakiran.com", Password = "HashedPassword123!", CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user2Id, FirstName = "Ayşe", LastName = "Kaya", Email = "ayse.kaya@dalgakiran.com", Password = "HashedPassword123!", CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user3Id, FirstName = "Mehmet", LastName = "Demir", Email = "mehmet.demir@dalgakiran.com", Password = "HashedPassword123!", CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user4Id, FirstName = "Zeynep", LastName = "Şahin", Email = "zeynep.sahin@dalgakiran.com", Password = "HashedPassword123!", CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user5Id, FirstName = "Burak", LastName = "Yılmaz", Email = "burak.yilmaz@dalgakiran.com", Password = "HashedPassword123!", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // Seed UserRoles - İlk kullanıcı Admin, ikinci kullanıcı Kullanıcı
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), UserId = user1Id, RoleId = adminRoleId, CreatedAt = seedDate, UpdatedAt = seedDate },
            new UserRole { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), UserId = user2Id, RoleId = userRoleId, CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // Seed UserEquipmentRequests - 2 records
        modelBuilder.Entity<UserEquipmentRequest>().HasData(
            new UserEquipmentRequest { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), UserId = user1Id, EquipmentId = laptop1Id, Description = "Dizüstü bilgisayarın ekranında sorun var, değişim gerekiyor", Priority = Domain.Enums.Priority.High, CreatedAt = seedDate, UpdatedAt = seedDate },
            new UserEquipmentRequest { Id = Guid.Parse("abcdefab-abcd-abcd-abcd-abcdefabcdef"), UserId = user2Id, EquipmentId = printer1Id, Description = "Yazıcı toner değişimi ve bakım talebi", Priority = Domain.Enums.Priority.Medium, CreatedAt = seedDate, UpdatedAt = seedDate }
        );
    }
}
