using System.ComponentModel.DataAnnotations;

namespace DotnetTest.Models;

// Product tablosu market ürünlerini tutar.
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ürün adı zorunludur.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 100000, ErrorMessage = "Fiyat 1'den büyük olmalıdır.")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Stok negatif olamaz.")]
    public int Stock { get; set; }

    [StringLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    // Foreign key: Product hangi kategoriye ait?
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
