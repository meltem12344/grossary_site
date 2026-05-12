using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotnetTest.ViewModels;

// Ürün ekleme ve güncelleme sayfalarında bu ViewModel kullanılır.
public class ProductFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ürün adı zorunludur.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    public string Description { get; set; } = string.Empty;

    [Range(1, 100000, ErrorMessage = "Fiyat 1'den büyük olmalıdır.")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Stok negatif olamaz.")]
    public int Stock { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Ürün türü seçiniz.")]
    public int CategoryId { get; set; }

    // SelectListItem, ürün türü dropdown'ı için kullanılır.
    public List<SelectListItem> Categories { get; set; } = new();
}
