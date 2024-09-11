using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecyclableMaterials.Migrations
{
    /// <inheritdoc />
    public partial class NotiProIdadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProductId",
                table: "Notifications",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Products_ProductId",
                table: "Notifications",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Products_ProductId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ProductId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Notifications");
        }
    }
}
