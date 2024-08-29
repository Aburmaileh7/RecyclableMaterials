using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecyclableMaterials.Migrations
{
    /// <inheritdoc />
    public partial class updateRatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ratings",
                newName: "RatingId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
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
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "Ratings",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
                column: "UserId",
                principalSchema: "sec",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
