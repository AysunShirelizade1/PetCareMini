using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMultilanguageToVeterinaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Veterinarians",
                newName: "SpecialtyEn");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Veterinarians",
                newName: "BioEn");

            migrationBuilder.AddColumn<string>(
                name: "BioAz",
                table: "Veterinarians",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialtyAz",
                table: "Veterinarians",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 27, 13, 19, 54, 547, DateTimeKind.Local).AddTicks(11));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BioAz",
                table: "Veterinarians");

            migrationBuilder.DropColumn(
                name: "SpecialtyAz",
                table: "Veterinarians");

            migrationBuilder.RenameColumn(
                name: "SpecialtyEn",
                table: "Veterinarians",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "BioEn",
                table: "Veterinarians",
                newName: "Bio");

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 13, 19, 59, 26, 110, DateTimeKind.Local).AddTicks(3281));
        }
    }
}

