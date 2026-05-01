using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FinalMultilangFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Services",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Services",
                newName: "DescriptionEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductCategories",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ProductCategories",
                newName: "DescriptionEn");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAz",
                table: "Services",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "Services",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAz",
                table: "ProductCategories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "ProductCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAz",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DescriptionAz",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "ProductCategories");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Services",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DescriptionEn",
                table: "Services",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "ProductCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DescriptionEn",
                table: "ProductCategories",
                newName: "Description");
        }
    }
}
