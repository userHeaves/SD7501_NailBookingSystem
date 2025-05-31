using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD7501_NailBookingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addAddOnPricesOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AddOnPrice",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddOnPrice",
                table: "OrderDetails");
        }
    }
}
