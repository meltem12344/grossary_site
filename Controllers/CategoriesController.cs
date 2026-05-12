using DotnetTest.Data;
using DotnetTest.Models;
using DotnetTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Controllers;

public class CategoriesController : Controller
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.Include(x => x.Products).ToList();
        return View(categories);
    }

    public IActionResult Details(int id)
    {
        var category = _context.Categories
            .Include(x => x.Products)
            .FirstOrDefault(x => x.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    public IActionResult Create()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Create(CategoryViewModel model)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _context.Categories.Add(new Category { Name = model.Name });
        _context.SaveChanges();

        TempData["Success"] = "Kategori eklendi.";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        return View(new CategoryViewModel { Id = category.Id, Name = category.Name });
    }

    [HttpPost]
    public IActionResult Edit(CategoryViewModel model)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var category = _context.Categories.Find(model.Id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = model.Name;
        _context.SaveChanges();

        TempData["Success"] = "Kategori güncellendi.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        TempData["Success"] = "Kategori silindi.";
        return RedirectToAction("Index");
    }

    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("Role") == "Admin";
    }
}
