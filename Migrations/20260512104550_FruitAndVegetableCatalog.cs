using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetTest.Migrations
{
    /// <inheritdoc />
    public partial class FruitAndVegetableCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Meyve");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sebze");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Price", "Stock" },
                values: new object[] { "Tatlı ve sulu kırmızı elma.", "", 34.90m, 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "ImageUrl", "Price", "Stock" },
                values: new object[] { 2, "", 28.50m, 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 1, "Olgun ve lezzetli muz.", "", "Muz", 49.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 2, "Çıtır ve taze salatalık.", "", "Salatalık", 22.90m, 75 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 5, 1, "Bol sulu portakal.", "", "Portakal", 32.90m, 70 },
                    { 6, 1, "Kokulu taze çilek.", "", "Çilek", 69.90m, 35 },
                    { 7, 1, "Yumuşak ve tatlı armut.", "", "Armut", 38.90m, 55 },
                    { 8, 1, "Tatlı sofralık üzüm.", "", "Üzüm", 44.90m, 45 },
                    { 9, 1, "Ekşi tatlı taze kivi.", "", "Kivi", 59.90m, 30 },
                    { 10, 1, "Sulu yaz şeftalisi.", "", "Şeftali", 42.90m, 40 },
                    { 11, 1, "Tatlı ve serin karpuz.", "", "Karpuz", 14.90m, 120 },
                    { 12, 1, "Bol sulu limon.", "", "Limon", 24.90m, 65 },
                    { 13, 2, "Yemeklik taze patates.", "", "Patates", 19.90m, 110 },
                    { 14, 2, "Kuru yemeklik soğan.", "", "Soğan", 17.90m, 95 },
                    { 15, 2, "Taze yeşil biber.", "", "Biber", 36.90m, 50 },
                    { 16, 2, "Çıtır taze havuç.", "", "Havuç", 21.90m, 70 },
                    { 17, 2, "Canlı ve taze marul.", "", "Marul", 18.90m, 45 },
                    { 18, 2, "Taze brokoli.", "", "Brokoli", 39.90m, 35 },
                    { 19, 2, "Yemeklik taze kabak.", "", "Kabak", 26.90m, 55 },
                    { 20, 2, "Parlak ve taze patlıcan.", "", "Patlıcan", 29.90m, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Meyve Sebze");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Süt Ürünleri");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Atıştırmalık" },
                    { 4, "Fırın" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Price", "Stock" },
                values: new object[] { "Taze kırmızı elma.", "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=600", 12.90m, 42 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "ImageUrl", "Price", "Stock" },
                values: new object[] { 1, "https://images.unsplash.com/photo-1546470427-e26264be0b0d?w=600", 8.50m, 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, "Günlük 1 litre süt.", "https://images.unsplash.com/photo-1563636619-e9143da7973b?w=600", "Süt 1L", 24m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { 4, "Fırından taze ekmek.", "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600", "Ekmek", 7.50m, 70 });
        }
    }
}
