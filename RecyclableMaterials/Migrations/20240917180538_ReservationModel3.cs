using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecyclableMaterials.Migrations
{
    /// <inheritdoc />
    public partial class ReservationModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationModel_Products_ProductId",
                table: "ReservationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationModel_Users_UserId",
                table: "ReservationModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationModel",
                table: "ReservationModel");

            migrationBuilder.RenameTable(
                name: "ReservationModel",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationModel_UserId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationModel_ProductId",
                table: "Reservations",
                newName: "IX_Reservations_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Products_ProductId",
                table: "Reservations",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalSchema: "sec",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Products_ProductId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "ReservationModel");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "ReservationModel",
                newName: "IX_ReservationModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ProductId",
                table: "ReservationModel",
                newName: "IX_ReservationModel_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationModel",
                table: "ReservationModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationModel_Products_ProductId",
                table: "ReservationModel",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationModel_Users_UserId",
                table: "ReservationModel",
                column: "UserId",
                principalSchema: "sec",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
