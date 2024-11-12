using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexForEachFKAndIsRequiredForSomeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                newName: "IX_Table_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                newName: "IX_Reservation_TableId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RestaurantId",
                table: "Reservations",
                newName: "IX_Reservation_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                newName: "IX_Reservation_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders",
                newName: "IX_Order_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                newName: "IX_Order_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                newName: "IX_OrderItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_RestaurantId",
                table: "MenuItems",
                newName: "IX_MenuItem_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RestaurantId",
                table: "Employees",
                newName: "IX_Employee_RestaurantId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Restaurants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total Amount",
                table: "Orders",
                type: "DECIMAL(15,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(15,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MenuItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Last Name",
                table: "Employees",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "First Name",
                table: "Employees",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone Number",
                table: "Customers",
                type: "VARCHAR(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Last Name",
                table: "Customers",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "First Name",
                table: "Customers",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "VARCHAR(320)",
                maxLength: 320,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(320)",
                oldMaxLength: 320,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Table_RestaurantId",
                table: "Tables",
                newName: "IX_Tables_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_TableId",
                table: "Reservations",
                newName: "IX_Reservations_TableId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_RestaurantId",
                table: "Reservations",
                newName: "IX_Reservations_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservations",
                newName: "IX_Reservations_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ReservationId",
                table: "Orders",
                newName: "IX_Orders_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_EmployeeId",
                table: "Orders",
                newName: "IX_Orders_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItems",
                newName: "IX_OrderItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_RestaurantId",
                table: "MenuItems",
                newName: "IX_MenuItems_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_RestaurantId",
                table: "Employees",
                newName: "IX_Employees_RestaurantId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Restaurants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total Amount",
                table: "Orders",
                type: "DECIMAL(15,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(15,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MenuItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Last Name",
                table: "Employees",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "First Name",
                table: "Employees",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "Phone Number",
                table: "Customers",
                type: "VARCHAR(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Last Name",
                table: "Customers",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "First Name",
                table: "Customers",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "VARCHAR(320)",
                maxLength: 320,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(320)",
                oldMaxLength: 320);
        }
    }
}
