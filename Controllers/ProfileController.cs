using DotnetTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Controllers;

public class ProfileController : Controller
{
    private readonly AppDbContext _context;

    public ProfileController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = _context.AppUsers.FirstOrDefault(x => x.Id == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(user);
    }

    public IActionResult Orders()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = _context.AppUsers
            .Include(x => x.Orders)
            .ThenInclude(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x!.Category)
            .FirstOrDefault(x => x.Id == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(user);
    }

    public IActionResult OrderDetails(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var order = _context.Orders
            .Include(x => x.AppUser)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x!.Category)
            .FirstOrDefault(x => x.Id == id && x.AppUserId == userId.Value);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpPost]
    public IActionResult Update(string fullName, string email, string phone, string address, string newPassword)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = _context.AppUsers.Find(userId.Value);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var cleanEmail = email.Trim().ToLower();
        var emailTaken = _context.AppUsers.Any(x => x.Email.ToLower() == cleanEmail && x.Id != user.Id);
        if (emailTaken)
        {
            TempData["Error"] = "Bu e-posta adresi başka biri tarafından kullanılıyor.";
            return RedirectToAction("Index");
        }

        user.FullName = fullName;
        user.Email = cleanEmail;
        user.UserName = cleanEmail.Split('@')[0];
        user.Phone = phone;
        user.Address = address;

        if (!string.IsNullOrWhiteSpace(newPassword))
        {
            user.Password = newPassword;
        }

        _context.SaveChanges();

        HttpContext.Session.SetString("UserName", user.FullName);
        TempData["Success"] = "Profil bilgileriniz güncellendi.";

        return RedirectToAction("Index");
    }
}
