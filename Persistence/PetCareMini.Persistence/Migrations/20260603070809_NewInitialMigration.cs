using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Veterinarians",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_UserId",
                table: "Veterinarians",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarians_Users_UserId",
                table: "Veterinarians",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarians_Users_UserId",
                table: "Veterinarians");

            migrationBuilder.DropIndex(
                name: "IX_Veterinarians_UserId",
                table: "Veterinarians");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Veterinarians");
        }
    }
}
