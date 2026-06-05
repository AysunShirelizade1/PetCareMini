using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Orders"" ADD COLUMN ""StatusTemp"" integer NOT NULL DEFAULT 0;
                
                UPDATE ""Orders"" SET ""StatusTemp"" = CASE ""Status""
                    WHEN 'Pending'   THEN 0
                    WHEN 'Accepted'  THEN 1
                    WHEN 'Shipped'   THEN 2
                    WHEN 'Delivered' THEN 3
                    WHEN 'Rejected'  THEN 4
                    WHEN 'Cancelled' THEN 5
                    ELSE 0
                END;
                
                ALTER TABLE ""Orders"" DROP COLUMN ""Status"";
                ALTER TABLE ""Orders"" RENAME COLUMN ""StatusTemp"" TO ""Status"";
            ");

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 5, 18, 25, 32, 3, DateTimeKind.Local).AddTicks(8557));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "Orders");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Orders"" ADD COLUMN ""StatusTemp"" character varying(50) NOT NULL DEFAULT 'Pending';
                
                UPDATE ""Orders"" SET ""StatusTemp"" = CASE ""Status""
                    WHEN 0 THEN 'Pending'
                    WHEN 1 THEN 'Accepted'
                    WHEN 2 THEN 'Shipped'
                    WHEN 3 THEN 'Delivered'
                    WHEN 4 THEN 'Rejected'
                    WHEN 5 THEN 'Cancelled'
                    ELSE 'Pending'
                END;
                
                ALTER TABLE ""Orders"" DROP COLUMN ""Status"";
                ALTER TABLE ""Orders"" RENAME COLUMN ""StatusTemp"" TO ""Status"";
            ");

            migrationBuilder.UpdateData(
                table: "ContactInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 5, 12, 34, 17, 833, DateTimeKind.Local).AddTicks(8533));
        }
    }
}