using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteUserVeterinarian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarians_Users_UserId",
                table: "Veterinarians");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Veterinarians",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 8, 9, 32, 17, 671, DateTimeKind.Local).AddTicks(3101));

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_UserId1",
                table: "Veterinarians",
                column: "UserId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarians_Users_UserId",
                table: "Veterinarians",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarians_Users_UserId1",
                table: "Veterinarians",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarians_Users_UserId",
                table: "Veterinarians");

            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarians_Users_UserId1",
                table: "Veterinarians");

            migrationBuilder.DropIndex(
                name: "IX_Veterinarians_UserId1",
                table: "Veterinarians");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Veterinarians");

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 7, 16, 40, 21, 486, DateTimeKind.Local).AddTicks(5560));

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarians_Users_UserId",
                table: "Veterinarians",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
