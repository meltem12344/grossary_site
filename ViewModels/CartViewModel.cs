namespace DotnetTest.ViewModels;

// Sepet sayfasında ürünleri ve toplam tutarı göstermek için kullanılır.
public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(x => x.LineTotal);
}

public class CartItemViewModel
{
    public int CartItemId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; } = "adet";
    public decimal LineTotal => Price * Quantity;
}
