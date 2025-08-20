using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEquipment> UserEquipments { get; set; }
    public DbSet<UserEquipmentRequest> UserEquipmentRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
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


        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = laptop1Id, Name = "Dizüstü Bilgisayar", Brand = "Dell", Model = "Latitude 7420", SerialNumber = "DL001", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = monitor1Id, Name = "Monitör", Brand = "Samsung", Model = "27UC850", SerialNumber = "SM002", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = keyboard1Id, Name = "Klavye", Brand = "Logitech", Model = "MX Keys", SerialNumber = "LG003", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = mouse1Id, Name = "Fare", Brand = "Logitech", Model = "MX Master 3", SerialNumber = "LG004", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Equipment { Id = printer1Id, Name = "Yazıcı", Brand = "HP", Model = "LaserJet Pro", SerialNumber = "HP005", CreatedAt = seedDate, UpdatedAt = seedDate }
        );
        
        modelBuilder.Entity<User>().HasData(
            new User { Id = user1Id, FirstName = "Ali", LastName = "Veli", Email = "ali.veli@dalgakiran.com", Password = "HashedPassword123!", Role = Domain.Enums.Role.Admin, CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user2Id, FirstName = "Ayşe", LastName = "Kaya", Email = "ayse.kaya@dalgakiran.com", Password = "HashedPassword123!", Role = Domain.Enums.Role.User, CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user3Id, FirstName = "Mehmet", LastName = "Demir", Email = "mehmet.demir@dalgakiran.com", Password = "HashedPassword123!", Role = Domain.Enums.Role.User, CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user4Id, FirstName = "Zeynep", LastName = "Şahin", Email = "zeynep.sahin@dalgakiran.com", Password = "HashedPassword123!", Role = Domain.Enums.Role.Admin, CreatedAt = seedDate, UpdatedAt = seedDate },
            new User { Id = user5Id, FirstName = "Burak", LastName = "Yılmaz", Email = "burak.yilmaz@dalgakiran.com", Password = "HashedPassword123!", Role = Domain.Enums.Role.User, CreatedAt = seedDate, UpdatedAt = seedDate }
        );
        
        modelBuilder.Entity<UserEquipment>().HasData(
            new UserEquipment { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), UserId = user2Id, EquipmentId = printer1Id, CreatedAt = seedDate, UpdatedAt = seedDate },
            new UserEquipment { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), UserId = user2Id, EquipmentId = laptop1Id, CreatedAt = seedDate, UpdatedAt = seedDate },
            new UserEquipment { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), UserId = user3Id, EquipmentId = mouse1Id, CreatedAt = seedDate, UpdatedAt = seedDate }
        );
        
        modelBuilder.Entity<UserEquipmentRequest>().HasData(
            new UserEquipmentRequest { Id = Guid.Parse("abcdefab-abcd-abcd-abcd-abcdefabcdef"), UserId = user2Id, EquipmentId = printer1Id, Description = "Yazıcı toner değişimi ve bakım talebi", Priority = Domain.Enums.Priority.Medium, Status = Domain.Enums.EquipmentStatus.pending, CreatedAt = seedDate, UpdatedAt = seedDate },
            new UserEquipmentRequest { Id = Guid.Parse("0198c7e0-5135-7f41-8e88-d901847716ef"), UserId = user2Id, EquipmentId = mouse1Id, Description = "Acil klavye bozuldu", Priority = Domain.Enums.Priority.High, Status = Domain.Enums.EquipmentStatus.approved, CreatedAt = new DateTime(2025, 8, 20, 14, 27, 3, 605, DateTimeKind.Utc).AddTicks(9470), UpdatedAt = new DateTime(2025, 8, 20, 14, 27, 3, 605, DateTimeKind.Utc).AddTicks(9470) },
            new UserEquipmentRequest { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), UserId = user1Id, EquipmentId = laptop1Id, Description = "Dizüstü bilgisayarın ekranında sorun var, değişim gerekiyor", Priority = Domain.Enums.Priority.High, Status = Domain.Enums.EquipmentStatus.pending, CreatedAt = seedDate, UpdatedAt = seedDate }
        );
    }
}
