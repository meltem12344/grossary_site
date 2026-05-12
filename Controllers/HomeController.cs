using DotnetTest.Data;
using DotnetTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string? q)
    {
        // ViewBag, ViewData ve TempData veri taşıma yöntemleri derste istenen konulardandır.
        ViewBag.PageTitle = "Market Ürünleri";
        ViewData["CategoryCount"] = _context.Categories.Count();
        ViewBag.SearchText = q;
        ViewBag.Categories = _context.Categories.ToList();

        var productsQuery = _context.Products
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var searchText = q.Trim().ToLower();

            productsQuery = productsQuery.Where(x =>
                x.Name.ToLower().Contains(searchText) ||
                x.Description.ToLower().Contains(searchText) ||
                (x.Category != null && x.Category.Name.ToLower().Contains(searchText)));
        }

        var products = productsQuery.ToList();

        return View(products);
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products
            .Include(x => x.Category)
            .FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
