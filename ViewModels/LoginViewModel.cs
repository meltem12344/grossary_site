using System.ComponentModel.DataAnnotations;

namespace DotnetTest.ViewModels;

// Login sayfasında sadece e-posta ve şifre istiyoruz.
public class LoginViewModel
{
    [Required(ErrorMessage = "E-posta zorunludur.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
