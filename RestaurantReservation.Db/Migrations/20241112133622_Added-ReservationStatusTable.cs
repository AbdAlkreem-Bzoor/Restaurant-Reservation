using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddedReservationStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationsStatus",
                columns: table => new
                {
                    ReservationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateOnly>(name: "Status Date", type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationsStatus", x => x.ReservationStatusId);
                    table.ForeignKey(
                        name: "FK_ReservationsStatus_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ReservationsStatus",
                columns: new[] { "ReservationStatusId", "ReservationId", "Status", "Status Date" },
                values: new object[,]
                {
                    { 1, 1, "NotAttended", new DateOnly(2024, 11, 12) },
                    { 2, 2, "Completed", new DateOnly(2024, 11, 10) },
                    { 3, 3, "Cancelled", new DateOnly(2024, 11, 9) },
                    { 4, 4, "Confirmed", new DateOnly(2024, 11, 11) },
                    { 5, 5, "Pending", new DateOnly(2024, 11, 12) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsStatus_ReservationId",
                table: "ReservationsStatus",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationsStatus");
        }
    }
}
