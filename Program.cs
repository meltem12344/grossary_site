using DotnetTest.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// MVC yapisini aktif ediyoruz. Controller ve View dosyalari bu sayede calisir.
builder.Services.AddControllersWithViews();

// Code First icin DbContext'i projeye tanitiyoruz.
// Connection string appsettings.json icinden okunur.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Basit login islemi icin Session kullaniyoruz.
// Bu proje ders projesi oldugu icin ASP.NET Identity yerine daha anlasilir bir yol secildi.
builder.Services.AddSession();

var app = builder.Build();

// Fiyat alanlarinda 38,90 gibi Turkce ondalikli sayilarin kabul edilmesi icin.
var turkishCulture = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = turkishCulture;
CultureInfo.DefaultThreadCurrentUICulture = turkishCulture;

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(turkishCulture),
    SupportedCultures = new[] { turkishCulture },
    SupportedUICultures = new[] { turkishCulture }
});

// Session, UseRouting ile MapControllerRoute arasinda olmali.
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
