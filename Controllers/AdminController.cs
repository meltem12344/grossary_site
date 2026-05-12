using DotnetTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        ViewBag.ProductCount = _context.Products.Count();
        ViewBag.OrderCount = _context.Orders.Count();
        ViewBag.UserCount = _context.AppUsers.Count();
        ViewBag.TotalRevenue = _context.Orders.Sum(x => (decimal?)x.TotalPrice) ?? 0;

        var lastOrders = _context.Orders
            .Include(x => x.AppUser)
            .OrderByDescending(x => x.OrderDate)
            .Take(5)
            .ToList();

        return View(lastOrders);
    }

    public IActionResult Orders()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var orders = _context.Orders
            .Include(x => x.AppUser)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x!.Category)
            .OrderByDescending(x => x.OrderDate)
            .ToList();

        return View(orders);
    }

    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("Role") == "Admin";
    }
}
