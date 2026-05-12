using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetTest.Migrations
{
    /// <inheritdoc />
    public partial class AddAhmetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Address", "Email", "FullName", "Password", "Phone", "Role", "UserName" },
                values: new object[] { 10, "Düzce Orhangazi mahallesi", "ahmetyilmaz@gmail.com", "Ahmet Yılmaz", "123456", "05461825011", "User", "ahmetyilmaz" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
