using DotnetTest.Data;
using DotnetTest.Models;
using DotnetTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        var products = _context.Products
            .Include(x => x.Category)
            .OrderBy(x => x.Category!.Name)
            .ThenBy(x => x.Name)
            .ToList();

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
            Categories = GetProductTypeList()
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

        ReadPriceFromForm(model);

        if (!ModelState.IsValid)
        {
            model.Categories = GetProductTypeList();
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name.Trim(),
            Description = model.Description.Trim(),
            Price = model.Price,
            Stock = model.Stock,
            ImageUrl = model.ImageUrl?.Trim() ?? string.Empty,
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
            Categories = GetProductTypeList()
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

        ReadPriceFromForm(model);

        if (!ModelState.IsValid)
        {
            model.Categories = GetProductTypeList();
            return View(model);
        }

        var product = _context.Products.Find(model.Id);
        if (product == null)
        {
            return NotFound();
        }

        product.Name = model.Name.Trim();
        product.Description = model.Description.Trim();
        product.Price = model.Price;
        product.Stock = model.Stock;
        product.ImageUrl = model.ImageUrl?.Trim() ?? string.Empty;
        product.CategoryId = model.CategoryId;

        _context.SaveChanges();

        TempData["Success"] = "Ürün güncellendi.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AutoSave(ProductFormViewModel model)
    {
        if (!IsAdmin())
        {
            return Unauthorized();
        }

        ReadPriceFromForm(model);

        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, message = "Bilgileri kontrol edin." });
        }

        var product = _context.Products.Find(model.Id);
        if (product == null)
        {
            return NotFound(new { success = false, message = "Ürün bulunamadı." });
        }

        product.Name = model.Name.Trim();
        product.Description = model.Description.Trim();
        product.Price = model.Price;
        product.Stock = model.Stock;
        product.ImageUrl = model.ImageUrl?.Trim() ?? string.Empty;
        product.CategoryId = model.CategoryId;

        _context.SaveChanges();

        return Json(new
        {
            success = true,
            message = "Otomatik kaydedildi.",
            savedAt = DateTime.Now.ToString("HH:mm:ss")
        });
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

        var hasOrder = _context.OrderItems.Any(x => x.ProductId == id);
        if (hasOrder)
        {
            TempData["Error"] = "Bu ürün daha önce siparişlerde kullanıldığı için silinemez. İstersen stok değerini 0 yapabilirsiniz.";
            return RedirectToAction("Index");
        }

        var cartItems = _context.CartItems.Where(x => x.ProductId == id).ToList();
        _context.CartItems.RemoveRange(cartItems);
        _context.Products.Remove(product);
        _context.SaveChanges();

        TempData["Success"] = "Ürün silindi.";
        return RedirectToAction("Index");
    }

    private List<SelectListItem> GetProductTypeList()
    {
        return _context.Categories
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            .ToList();
    }

    private void ReadPriceFromForm(ProductFormViewModel model)
    {
        var rawPrice = Request.Form[nameof(ProductFormViewModel.Price)].ToString();

        if (string.IsNullOrWhiteSpace(rawPrice))
        {
            ModelState.Remove(nameof(ProductFormViewModel.Price));
            ModelState.AddModelError(nameof(ProductFormViewModel.Price), "Fiyat zorunludur.");
            return;
        }

        var trCulture = new CultureInfo("tr-TR");
        var normalizedPrice = rawPrice.Trim();

        var parsed =
            decimal.TryParse(normalizedPrice, NumberStyles.Number, trCulture, out var price) ||
            decimal.TryParse(normalizedPrice.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out price);

        ModelState.Remove(nameof(ProductFormViewModel.Price));

        if (!parsed)
        {
            ModelState.AddModelError(nameof(ProductFormViewModel.Price), "Fiyatı 38,90 şeklinde yazabilirsiniz.");
            return;
        }

        if (price < 1 || price > 100000)
        {
            ModelState.AddModelError(nameof(ProductFormViewModel.Price), "Fiyat 1 ile 100000 arasında olmalıdır.");
            return;
        }

        model.Price = price;
    }

    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("Role") == "Admin";
    }
}
