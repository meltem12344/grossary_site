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
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Meyve Sebze" },
            new Category { Id = 2, Name = "Süt Ürünleri" },
            new Category { Id = 3, Name = "Atıştırmalık" },
            new Category { Id = 4, Name = "Fırın" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Kırmızı Elma",
                Description = "Taze kırmızı elma.",
                Price = 12.90m,
                Stock = 42,
                ImageUrl = "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=600",
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Domates",
                Description = "Günlük taze domates.",
                Price = 8.50m,
                Stock = 55,
                ImageUrl = "https://images.unsplash.com/photo-1546470427-e26264be0b0d?w=600",
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "Süt 1L",
                Description = "Günlük 1 litre süt.",
                Price = 24,
                Stock = 60,
                ImageUrl = "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=600",
                CategoryId = 2
            },
            new Product
            {
                Id = 4,
                Name = "Ekmek",
                Description = "Fırından taze ekmek.",
                Price = 7.50m,
                Stock = 70,
                ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600",
                CategoryId = 4
            }
        );
    }
}
