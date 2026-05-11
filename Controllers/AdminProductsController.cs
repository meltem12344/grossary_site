using DotnetTest.Data;
using DotnetTest.Models;
using DotnetTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Controllers;

public class AdminProductsController : Controller
{
    private readonly AppDbContext _context;

    public AdminProductsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var products = _context.Products.Include(x => x.Category).ToList();
        return View(products);
    }

    public IActionResult Details(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult Create()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var model = new ProductFormViewModel
        {
            Categories = GetCategoryList()
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Create(ProductFormViewModel model)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            model.Categories = GetCategoryList();
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        TempData["Success"] = "Ürün eklendi.";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Categories = GetCategoryList()
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(ProductFormViewModel model)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            model.Categories = GetCategoryList();
            return View(model);
        }

        var product = _context.Products.Find(model.Id);
        if (product == null)
        {
            return NotFound();
        }

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.Stock = model.Stock;
        product.ImageUrl = model.ImageUrl;
        product.CategoryId = model.CategoryId;

        _context.SaveChanges();

        TempData["Success"] = "Ürün güncellendi.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        TempData["Success"] = "Ürün silindi.";
        return RedirectToAction("Index");
    }

    private List<SelectListItem> GetCategoryList()
    {
        return _context.Categories
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            .ToList();
    }

    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("Role") == "Admin";
    }
}
