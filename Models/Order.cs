namespace DotnetTest.Models;

// Sipariş ana tablosu. Bir siparişin tarihi ve toplam tutarı burada durur.
public class Order
{
    public int Id { get; set; }

    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }

    public string DeliveryFullName { get; set; } = string.Empty;
    public string DeliveryEmail { get; set; } = string.Empty;
    public string DeliveryPhone { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;

    public List<OrderItem> OrderItems { get; set; } = new();
}
