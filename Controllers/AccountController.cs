using DotnetTest.Data;
using DotnetTest.Models;
using DotnetTest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTest.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var email = model.Email.Trim().ToLower();
        if (!IsValidEmail(email))
        {
            ModelState.AddModelError("Email", "Geçerli bir e-posta giriniz. Örnek: ahmet@gmail.com");
            return View(model);
        }

        // Yeni sistemde giriş e-posta ile yapılır.
        // Eski kullanıcılar sorun yaşamasın diye UserName kontrolü de yedek olarak duruyor.
        var user = _context.AppUsers.FirstOrDefault(x =>
            (x.Email.ToLower() == email || x.UserName.ToLower() == email) &&
            x.Password == model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("Role", user.Role);

        TempData["Success"] = "Giriş başarılı.";

        if (user.Role == "Admin")
        {
            return RedirectToAction("Index", "Admin");
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var email = model.Email.Trim().ToLower();
        if (!IsValidEmail(email))
        {
            ModelState.AddModelError("Email", "Geçerli bir e-posta giriniz. Örnek: ahmet@gmail.com");
            return View(model);
        }

        var sameEmailExists = _context.AppUsers.Any(x => x.Email.ToLower() == email);
        if (sameEmailExists)
        {
            ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
            return View(model);
        }

        var user = new AppUser
        {
            FullName = model.FullName,
            Email = email,
            UserName = email.Split('@')[0],
            Phone = model.Phone,
            Address = model.Address,
            Password = model.Password,
            Role = "User"
        };

        _context.AppUsers.Add(user);
        _context.SaveChanges();

        TempData["Success"] = "Kayıt başarılı. Şimdi e-posta adresinizle giriş yapabilirsiniz.";
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["Success"] = "Çıkış yapıldı.";
        return RedirectToAction("Login");
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains('@') && email.Contains('.') && !email.Contains(' ');
    }
}
