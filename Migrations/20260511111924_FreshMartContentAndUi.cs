using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetTest.Migrations
{
    /// <inheritdoc />
    public partial class FreshMartContentAndUi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Fırın" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price", "Stock" },
                values: new object[] { "Kırmızı Elma", 12.90m, 42 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 1, "Günlük taze domates.", "https://images.unsplash.com/photo-1546470427-e26264be0b0d?w=600", "Domates", 8.50m, 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 2, "Günlük 1 litre süt.", "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=600", "Süt 1L", 24m, 60 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 4, 4, "Fırından taze ekmek.", "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600", "Ekmek", 7.50m, 70 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price", "Stock" },
                values: new object[] { "Elma", 25m, 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 2, "Günlük 1 litre süt.", "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=600", "Süt", 35m, 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 3, "Sütlü çikolata.", "https://images.unsplash.com/photo-1549007994-cb92caebd54b?w=600", "Çikolata", 20m, 80 });
        }
    }
}
