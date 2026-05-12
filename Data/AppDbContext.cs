using DotnetTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Data;

// DbContext sınıfı veritabanı ile C# kodları arasındaki köprüdür.
// Buradaki DbSet'ler veritabanında tablo olarak oluşur.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Para alanları SQL Server'da düzgün oluşsun diye decimal ayarı yapıyoruz.
        modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().Property(x => x.TotalPrice).HasPrecision(18, 2);
        modelBuilder.Entity<OrderItem>().Property(x => x.Price).HasPrecision(18, 2);

        // İlk migration çalışınca örnek kullanıcılar otomatik veritabanına eklenir.
        modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@freshmart.com",
                Password = "123",
                FullName = "Admin User",
                Phone = "0555 000 00 01",
                Address = "FreshMart merkez ofis",
                Role = "Admin"
            },
            new AppUser
            {
                Id = 2,
                UserName = "user",
                Email = "user@freshmart.com",
                Password = "123",
                FullName = "Normal User",
                Phone = "0555 000 00 02",
                Address = "Örnek mahalle, örnek sokak",
                Role = "User"
            },
            new AppUser
            {
                Id = 10,
                UserName = "ahmetyilmaz",
                Email = "ahmetyilmaz@gmail.com",
                Password = "123456",
                FullName = "Ahmet Yılmaz",
                Phone = "05461825011",
                Address = "Düzce Orhangazi mahallesi",
                Role = "User"
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Meyve" },
            new Category { Id = 2, Name = "Sebze" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Kırmızı Elma", Description = "Tatlı ve sulu kırmızı elma.", Price = 34.90m, Stock = 80, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 2, Name = "Domates", Description = "Günlük taze domates.", Price = 28.50m, Stock = 90, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 3, Name = "Muz", Description = "Olgun ve lezzetli muz.", Price = 49.90m, Stock = 60, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 4, Name = "Salatalık", Description = "Çıtır ve taze salatalık.", Price = 22.90m, Stock = 75, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 5, Name = "Portakal", Description = "Bol sulu portakal.", Price = 32.90m, Stock = 70, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 6, Name = "Çilek", Description = "Kokulu taze çilek.", Price = 69.90m, Stock = 35, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 7, Name = "Armut", Description = "Yumuşak ve tatlı armut.", Price = 38.90m, Stock = 55, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 8, Name = "Üzüm", Description = "Tatlı sofralık üzüm.", Price = 44.90m, Stock = 45, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 9, Name = "Kivi", Description = "Ekşi tatlı taze kivi.", Price = 59.90m, Stock = 30, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 10, Name = "Şeftali", Description = "Sulu yaz şeftalisi.", Price = 42.90m, Stock = 40, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 11, Name = "Karpuz", Description = "Tatlı ve serin karpuz.", Price = 14.90m, Stock = 120, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 12, Name = "Limon", Description = "Bol sulu limon.", Price = 24.90m, Stock = 65, ImageUrl = "", CategoryId = 1 },
            new Product { Id = 13, Name = "Patates", Description = "Yemeklik taze patates.", Price = 19.90m, Stock = 110, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 14, Name = "Soğan", Description = "Kuru yemeklik soğan.", Price = 17.90m, Stock = 95, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 15, Name = "Biber", Description = "Taze yeşil biber.", Price = 36.90m, Stock = 50, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 16, Name = "Havuç", Description = "Çıtır taze havuç.", Price = 21.90m, Stock = 70, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 17, Name = "Marul", Description = "Canlı ve taze marul.", Price = 18.90m, Stock = 45, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 18, Name = "Brokoli", Description = "Taze brokoli.", Price = 39.90m, Stock = 35, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 19, Name = "Kabak", Description = "Yemeklik taze kabak.", Price = 26.90m, Stock = 55, ImageUrl = "", CategoryId = 2 },
            new Product { Id = 20, Name = "Patlıcan", Description = "Parlak ve taze patlıcan.", Price = 29.90m, Stock = 50, ImageUrl = "", CategoryId = 2 }
        );
    }
}
