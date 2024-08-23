using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecyclableMaterials.Migrations
{
    /// <inheritdoc />
    public partial class productUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                schema: "dbo",
                table: "Products",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                schema: "dbo",
                table: "Products",
                column: "UserId",
                principalSchema: "sec",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Products");
        }
    }
}
