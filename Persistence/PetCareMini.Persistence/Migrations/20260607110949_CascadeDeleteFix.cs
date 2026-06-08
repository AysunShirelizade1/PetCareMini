using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_Users_UserId",
                table: "BlogComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_AuthorId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProductReviews",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 7, 15, 9, 48, 84, DateTimeKind.Local).AddTicks(2333));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_Users_UserId",
                table: "BlogComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_Users_UserId",
                table: "BlogComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_AuthorId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProductReviews",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 6, 2, 43, 4, 3, DateTimeKind.Local).AddTicks(9024));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_Users_UserId",
                table: "BlogComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
