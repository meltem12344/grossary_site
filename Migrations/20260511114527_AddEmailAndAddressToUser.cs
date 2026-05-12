using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetTest.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndAddressToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AppUsers",
                type: "TEXT",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            // Daha önce açılmış hesaplar e-postasız kalmasın diye geçici e-posta oluşturuyoruz.
            // Örnek: meltem -> meltem@freshmart.local
            migrationBuilder.Sql("UPDATE AppUsers SET Email = UserName || '@freshmart.local' WHERE Email = ''");
            migrationBuilder.Sql("UPDATE AppUsers SET Address = 'Adres bilgisi girilmedi' WHERE Address = ''");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email" },
                values: new object[] { "FreshMart merkez ofis", "admin@freshmart.com" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Email" },
                values: new object[] { "Örnek mahalle, örnek sokak", "user@freshmart.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppUsers");
        }
    }
}
