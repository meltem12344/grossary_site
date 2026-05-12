using System.ComponentModel.DataAnnotations;

namespace DotnetTest.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}
