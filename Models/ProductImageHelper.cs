using System.Globalization;
using System.Text;

namespace DotnetTest.Models;

// Urunlerde ImageUrl bos birakilirsa, urun adina gore otomatik uygun gorsel gosterir.
public static class ProductImageHelper
{
    public static string GetImage(Product? product)
    {
        return GetImage(product?.Name ?? string.Empty, product?.ImageUrl);
    }

    public static string GetImage(string name, string? imageUrl = null)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl))
        {
            return imageUrl;
        }

        var key = Normalize(name);

        if (key.Contains("elma")) return "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("domates")) return "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("muz")) return "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("salatalik")) return "https://images.unsplash.com/photo-1604977042946-1eecc30f269e?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("portakal")) return "https://images.unsplash.com/photo-1547514701-42782101795e?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("cilek")) return "https://images.unsplash.com/photo-1464965911861-746a04b4bca6?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("armut")) return "https://images.unsplash.com/photo-1514756331096-242fdeb70d4a?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("uzum")) return "https://images.unsplash.com/photo-1537640538966-79f369143f8f?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("kivi")) return "https://images.unsplash.com/photo-1585059895524-72359e06133a?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("seftali")) return "https://images.unsplash.com/photo-1532704868953-d85f24176d73?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("karpuz")) return "https://images.unsplash.com/photo-1563114773-84221bd62daa?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("limon")) return "https://images.unsplash.com/photo-1587496679742-bad502958fbf?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("patates")) return "https://images.unsplash.com/photo-1518977676601-b53f82aba655?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("sogan")) return "https://images.unsplash.com/photo-1508747703725-719777637510?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("biber")) return "https://images.unsplash.com/photo-1563565375-f3fdfdbefa83?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("havuc")) return "https://images.unsplash.com/photo-1445282768818-728615cc910a?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("marul")) return "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("brokoli")) return "https://images.unsplash.com/photo-1584270354949-c26b0d5b4a0c?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("kabak")) return "https://images.unsplash.com/photo-1590165482129-1b8b27698780?auto=format&fit=crop&w=900&q=80";
        if (key.Contains("patlican")) return "https://images.unsplash.com/photo-1615485290382-441e4d049cb5?auto=format&fit=crop&w=900&q=80";

        return "https://images.unsplash.com/photo-1542838132-92c53300491e?auto=format&fit=crop&w=900&q=80";
    }

    private static string Normalize(string value)
    {
        var lower = value.ToLower(new CultureInfo("tr-TR"));
        var normalized = lower.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();

        foreach (var character in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(character);
            }
        }

        return builder.ToString()
            .Replace("ı", "i")
            .Replace("Ä±", "i")
            .Replace("ÄŸ", "g")
            .Replace("ÅŸ", "s")
            .Replace("Ã§", "c")
            .Replace("Ã¼", "u")
            .Replace("Ã¶", "o");
    }
}
