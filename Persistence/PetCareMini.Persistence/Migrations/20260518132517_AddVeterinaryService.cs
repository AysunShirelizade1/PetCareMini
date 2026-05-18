using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetCareMini.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVeterinaryService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeterinaryReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VeterinarianId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    AppointmentId = table.Column<int>(type: "integer", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinaryReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinaryReviews_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VeterinaryReviews_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VeterinaryReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinaryReviews_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryReviews_AppointmentId",
                table: "VeterinaryReviews",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryReviews_ServiceId",
                table: "VeterinaryReviews",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryReviews_UserId",
                table: "VeterinaryReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryReviews_VeterinarianId",
                table: "VeterinaryReviews",
                column: "VeterinarianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeterinaryReviews");
        }
    }
}
