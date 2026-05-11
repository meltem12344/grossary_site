using DotnetTest.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC yapısını aktif ediyoruz. Controller ve View dosyaları bu sayede çalışır.
builder.Services.AddControllersWithViews();

// Code First için DbContext'i projeye tanıtıyoruz.
// Connection string appsettings.json içinden okunur.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Basit login işlemi için Session kullanıyoruz.
// Bu proje ders projesi olduğu için ASP.NET Identity yerine daha anlaşılır bir yol seçildi.
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session, UseRouting ile MapControllerRoute arasında olmalı.
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
