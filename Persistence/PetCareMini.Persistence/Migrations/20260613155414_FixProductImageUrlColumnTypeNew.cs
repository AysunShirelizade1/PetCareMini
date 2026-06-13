using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixProductImageUrlColumnTypeNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 13, 19, 54, 13, 774, DateTimeKind.Local).AddTicks(4304));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 13, 19, 31, 46, 285, DateTimeKind.Local).AddTicks(6342));
        }
    }
}
