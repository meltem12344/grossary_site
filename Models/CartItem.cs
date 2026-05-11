namespace DotnetTest.Models;

// Sepetteki her satır bir CartItem'dır.
// Örnek: Kullanıcı 2 adet elma eklediyse burada tutulur.
public class CartItem
{
    public int Id { get; set; }

    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
}
