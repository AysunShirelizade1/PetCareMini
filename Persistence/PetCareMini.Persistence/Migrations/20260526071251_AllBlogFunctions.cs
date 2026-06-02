using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AllBlogFunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_Slug",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "BlogTags",
                newName: "SlugEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BlogTags",
                newName: "SlugAz");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTags_Slug",
                table: "BlogTags",
                newName: "IX_BlogTags_SlugEn");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "BlogPosts",
                newName: "TitleEn");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "BlogPosts",
                newName: "SummaryEn");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "BlogPosts",
                newName: "TitleAz");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogPosts",
                newName: "ContentEn");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "BlogCategories",
                newName: "SlugEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BlogCategories",
                newName: "SlugAz");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "BlogCategories",
                newName: "DescriptionEn");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategories_Slug",
                table: "BlogCategories",
                newName: "IX_BlogCategories_SlugEn");

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "BlogTags",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "BlogTags",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentAz",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SlugAz",
                table: "BlogPosts",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SlugEn",
                table: "BlogPosts",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SummaryAz",
                table: "BlogPosts",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAz",
                table: "BlogCategories",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "BlogCategories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "BlogCategories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_SlugAz",
                table: "BlogTags",
                column: "SlugAz",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_SlugAz",
                table: "BlogPosts",
                column: "SlugAz",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_SlugEn",
                table: "BlogPosts",
                column: "SlugEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_SlugAz",
                table: "BlogCategories",
                column: "SlugAz",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogTags_SlugAz",
                table: "BlogTags");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_SlugAz",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_SlugEn",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_SlugAz",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "ContentAz",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SlugAz",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SlugEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SummaryAz",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "DescriptionAz",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "BlogCategories");

            migrationBuilder.RenameColumn(
                name: "SlugEn",
                table: "BlogTags",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "SlugAz",
                table: "BlogTags",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTags_SlugEn",
                table: "BlogTags",
                newName: "IX_BlogTags_Slug");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "BlogPosts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleAz",
                table: "BlogPosts",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "SummaryEn",
                table: "BlogPosts",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "ContentEn",
                table: "BlogPosts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "SlugEn",
                table: "BlogCategories",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "SlugAz",
                table: "BlogCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DescriptionEn",
                table: "BlogCategories",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategories_SlugEn",
                table: "BlogCategories",
                newName: "IX_BlogCategories_Slug");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_Slug",
                table: "BlogPosts",
                column: "Slug",
                unique: true);
        }
    }
}
