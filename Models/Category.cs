using System.ComponentModel.DataAnnotations;

namespace DotnetTest.Models;

// Kategori tablosu. Örnek: Meyve Sebze, Süt Ürünleri.
public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}
