using System.ComponentModel.DataAnnotations;

namespace DotnetTest.Models;

// Kullanıcı tablosu. Login ve profil işlemleri için kullanılır.
public class AppUser
{
    public int Id { get; set; }

    // Eski projede kullanıcı adı vardı. Artık giriş e-posta ile yapılır.
    // Bu alan yine tabloda kalıyor; e-postanın @ öncesinden otomatik dolduruyoruz.
    [Required]
    [StringLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [StringLength(300)]
    public string Address { get; set; } = string.Empty;

    // Role alanı Admin mi normal User mı anlamamızı sağlar.
    [Required]
    [StringLength(20)]
    public string Role { get; set; } = "User";

    public List<CartItem> CartItems { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}
