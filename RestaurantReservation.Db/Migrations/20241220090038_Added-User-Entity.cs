using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(name: "User Name", type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(320)", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role", "User Name" },
                values: new object[,]
                {
                    { 1, "john@example.com", "Password123", "Customer", "john_doe" },
                    { 2, "sarah@example.com", "Password123", "Employee", "sarah_smith" },
                    { 3, "michael@example.com", "Password123", "ResturantBoss", "michael_jones" },
                    { 4, "alice@example.com", "Password123", "Admin", "alice_johnson" },
                    { 5, "david@example.com", "Password123", "Customer", "david_white" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
