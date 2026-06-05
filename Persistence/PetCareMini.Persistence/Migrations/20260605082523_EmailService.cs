using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmailService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpireDate",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 5, 12, 25, 22, 648, DateTimeKind.Local).AddTicks(1382));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpireDate",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 4, 11, 46, 21, 613, DateTimeKind.Local).AddTicks(4485));
        }
    }
}
