using DotnetTest.Data;
using DotnetTest.Models;
using DotnetTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Controllers;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var items = _context.CartItems
            .Include(x => x.Product)
            .ThenInclude(x => x!.Category)
            .Where(x => x.AppUserId == userId)
            .ToList();

        var model = new CartViewModel
        {
            Items = items.Select(x => new CartItemViewModel
            {
                CartItemId = x.Id,
                ProductName = x.Product?.Name ?? "",
                ImageUrl = x.Product?.ImageUrl ?? "",
                Price = x.Product?.Price ?? 0,
                Quantity = x.Quantity,
                Unit = GetUnit(x.Product)
            }).ToList()
        };

        ViewBag.User = _context.AppUsers.Find(userId.Value);

        return View(model);
    }

    [HttpPost]
    public IActionResult Add(int productId, int quantity = 1)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            TempData["Error"] = "Sepete eklemek için giriş yapmalısınız.";
            return RedirectToAction("Login", "Account");
        }

        var product = _context.Products
            .Include(x => x.Category)
            .FirstOrDefault(x => x.Id == productId);

        if (product == null || product.Stock <= 0)
        {
            TempData["Error"] = "Ürün bulunamadı veya stok yok.";
            return RedirectToAction("Index", "Home");
        }

        if (quantity < 1)
        {
            quantity = 1;
        }

        var cartItem = _context.CartItems.FirstOrDefault(x =>
            x.AppUserId == userId && x.ProductId == productId);

        var currentQuantity = cartItem?.Quantity ?? 0;
        if (currentQuantity + quantity > product.Stock)
        {
            TempData["Error"] = $"Stokta sadece {product.Stock} {GetUnit(product)} var.";
            return RedirectBack();
        }

        if (cartItem == null)
        {
            _context.CartItems.Add(new CartItem
            {
                AppUserId = userId.Value,
                ProductId = productId,
                Quantity = quantity
            });
        }
        else
        {
            cartItem.Quantity += quantity;
        }

        _context.SaveChanges();
        TempData["Success"] = "Ürün sepete eklendi.";
        return RedirectBack();
    }

    [HttpPost]
    public IActionResult Increase(int id)
    {
        var item = _context.CartItems
            .Include(x => x.Product)
            .ThenInclude(x => x!.Category)
            .FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            if (item.Product != null && item.Quantity < item.Product.Stock)
            {
                item.Quantity++;
                _context.SaveChanges();
            }
            else if (item.Product != null)
            {
                TempData["Error"] = $"Stokta sadece {item.Product.Stock} {GetUnit(item.Product)} var.";
            }
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Decrease(int id)
    {
        var item = _context.CartItems.Find(id);
        if (item != null)
        {
            item.Quantity--;

            if (item.Quantity <= 0)
            {
                _context.CartItems.Remove(item);
            }

            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int id)
    {
        var item = _context.CartItems.Find(id);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult Checkout()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var hasCartItem = _context.CartItems.Any(x => x.AppUserId == userId);
        if (!hasCartItem)
        {
            TempData["Error"] = "Sepetiniz boş.";
            return RedirectToAction("Index");
        }

        var items = _context.CartItems
            .Include(x => x.Product)
            .ThenInclude(x => x!.Category)
            .Where(x => x.AppUserId == userId)
            .ToList();

        var model = new CartViewModel
        {
            Items = items.Select(x => new CartItemViewModel
            {
                CartItemId = x.Id,
                ProductName = x.Product?.Name ?? "",
                ImageUrl = x.Product?.ImageUrl ?? "",
                Price = x.Product?.Price ?? 0,
                Quantity = x.Quantity,
                Unit = GetUnit(x.Product)
            }).ToList()
        };

        ViewBag.User = _context.AppUsers.Find(userId.Value);

        return View(model);
    }

    [HttpPost]
    public IActionResult CheckoutConfirm(
        string deliveryFullName,
        string deliveryEmail,
        string deliveryPhone,
        string deliveryAddress)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = _context.AppUsers.Find(userId.Value);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        deliveryFullName = string.IsNullOrWhiteSpace(deliveryFullName) ? user.FullName : deliveryFullName.Trim();
        deliveryEmail = string.IsNullOrWhiteSpace(deliveryEmail) ? user.Email : deliveryEmail.Trim();
        deliveryPhone = string.IsNullOrWhiteSpace(deliveryPhone) ? user.Phone : deliveryPhone.Trim();
        deliveryAddress = string.IsNullOrWhiteSpace(deliveryAddress) ? user.Address : deliveryAddress.Trim();

        if (string.IsNullOrWhiteSpace(deliveryAddress))
        {
            TempData["Error"] = "Siparişi tamamlamak için teslimat adresi gereklidir.";
            return RedirectToAction("Checkout");
        }

        var cartItems = _context.CartItems
            .Include(x => x.Product)
            .ThenInclude(x => x!.Category)
            .Where(x => x.AppUserId == userId)
            .ToList();

        if (!cartItems.Any())
        {
            TempData["Error"] = "Sepetiniz boş.";
            return RedirectToAction("Index");
        }

        var order = new Order
        {
            AppUserId = userId.Value,
            OrderDate = DateTime.Now,
            TotalPrice = cartItems.Sum(x => (x.Product?.Price ?? 0) * x.Quantity),
            DeliveryFullName = deliveryFullName,
            DeliveryEmail = deliveryEmail,
            DeliveryPhone = deliveryPhone,
            DeliveryAddress = deliveryAddress
        };

        foreach (var item in cartItems)
        {
            if (item.Product == null)
            {
                continue;
            }

            order.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Product.Price
            });

            // Sipariş verilince stoktan düşüyoruz.
            item.Product.Stock -= item.Quantity;
        }

        _context.Orders.Add(order);
        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();

        TempData["Success"] = "Siparişiniz oluşturuldu.";
        return RedirectToAction("Index", "Profile");
    }

    private int? GetUserId()
    {
        return HttpContext.Session.GetInt32("UserId");
    }

    private IActionResult RedirectBack()
    {
        var returnUrl = Request.Headers.Referer.ToString();
        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    private string GetUnit(Product? product)
    {
        if (product?.Category?.Name.Contains("Meyve", StringComparison.OrdinalIgnoreCase) == true ||
            product?.Category?.Name.Contains("Sebze", StringComparison.OrdinalIgnoreCase) == true)
        {
            return "kg";
        }

        return "adet";
    }
}
