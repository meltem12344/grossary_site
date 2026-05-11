# Grocery Store Shopping Proje Notları

Bu proje ASP.NET Core MVC ile hazırlanmış basit bir market alışveriş projesidir.
Amaç, derste anlatılan MVC, Entity Framework Core, Code First, Migration, CRUD,
ViewModel, validation, layout ve veri taşıma konularını göstermektir.

## Giriş Bilgileri

- Admin kullanıcı: `admin`
- Admin şifre: `123`
- Normal kullanıcı: `user`
- Normal kullanıcı şifre: `123`

## Çalıştırma Komutları

```powershell
dotnet restore
dotnet tool restore
dotnet build
dotnet tool run dotnet-ef database update
dotnet run --urls http://localhost:5099
```

Tarayıcıda:

```text
http://localhost:5099
```

## Migration Komutları

İlk migration oluşturmak için:

```powershell
dotnet tool run dotnet-ef migrations add InitialCreate
```

Veritabanına uygulamak için:

```powershell
dotnet tool run dotnet-ef database update
```

Bu projede SQLite kullanıldı. Bu yüzden veritabanı dosyası proje klasöründe
`GroceryStore.db` olarak oluşur. SQL Server kurmadan çalışması için bu yol seçildi.

## Sayfalar

1. `Account/Login` - kullanıcı girişi
2. `Account/Register` - kullanıcı kaydı
3. `Home/Index` - ürün listeleme
4. `Home/Details` - ürün detayı
5. `Cart/Index` - sepet
6. `Cart/Checkout` - sipariş onayı
7. `Profile/Index` - profil ve sipariş geçmişi
8. `Categories/Index` - kategori listeleme
9. `Categories/Create/Edit/Delete/Details` - kategori CRUD
10. `AdminProducts/Index/Create/Edit/Delete/Details` - ürün CRUD
11. `Admin/Index` - admin dashboard
12. `Admin/Orders` - tüm siparişler

## Dosyalar Ne İşe Yarar?

### `Program.cs`

Projenin başlangıç dosyasıdır. MVC, DbContext ve Session burada aktif edilir.

### `Data/AppDbContext.cs`

Veritabanı bağlantısını temsil eder. `DbSet` alanları veritabanında tablo olur.
Örnek kullanıcı, kategori ve ürün verileri de burada eklenir.

### `Models`

Veritabanına gidecek ana tablolar:

- `AppUser.cs`: kullanıcı bilgileri
- `Category.cs`: kategori bilgileri
- `Product.cs`: ürün bilgileri
- `CartItem.cs`: sepet satırları
- `Order.cs`: sipariş ana bilgisi
- `OrderItem.cs`: sipariş ürün satırları

### `ViewModels`

Form sayfalarında kullanılır. Validation kuralları burada yazıldı.

- `LoginViewModel.cs`: login formu
- `RegisterViewModel.cs`: kayıt formu
- `ProductFormViewModel.cs`: ürün ekleme/güncelleme formu
- `CategoryViewModel.cs`: kategori formu
- `CartViewModel.cs`: sepet ekranı

### `Controllers`

Sayfalarda yapılacak işlemleri yönetir.

- `HomeController.cs`: ürün listeleme ve detay
- `AccountController.cs`: login, register, logout
- `CartController.cs`: sepete ekleme, silme, sipariş verme
- `ProfileController.cs`: kullanıcı profili ve sipariş geçmişi
- `CategoriesController.cs`: kategori CRUD
- `AdminProductsController.cs`: ürün CRUD
- `AdminController.cs`: dashboard ve sipariş listesi

### `Views`

Kullanıcının gördüğü sayfalardır.

- `Views/Shared/_UserLayout.cshtml`: normal kullanıcı tasarımı
- `Views/Shared/_AdminLayout.cshtml`: admin panel tasarımı
- `Views/Account`: login ve kayıt sayfaları
- `Views/Home`: ana sayfa ve ürün detay
- `Views/Cart`: sepet ve sipariş tamamlama
- `Views/Profile`: profil
- `Views/Categories`: kategori sayfaları
- `Views/AdminProducts`: admin ürün sayfaları
- `Views/Admin`: dashboard ve siparişler

## Hocaya Söylenebilecek Kısa Açıklama

Bu projede Code First yaklaşımı kullandım. Önce model classlarını yazdım,
sonra `AppDbContext` ile veritabanına bağladım. `InitialCreate` migration'ı ile
tablolar oluştu. Ürün ve kategori için ekleme, listeleme, güncelleme, silme ve
detay işlemleri var. Login için Session kullandım. User ve Admin için iki farklı
layout sayfası var. Formlarda ViewModel ve validation kullandım. Ayrıca ViewBag,
ViewData ve TempData ile veri taşıma örnekleri kullandım.
