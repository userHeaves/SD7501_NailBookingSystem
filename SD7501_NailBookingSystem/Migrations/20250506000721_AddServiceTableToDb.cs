using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SD7501_NailBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddOns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AddOns",
                columns: new[] { "Id", "Cost", "Type" },
                values: new object[,]
                {
                    { 1, 8m, "French Tip" },
                    { 2, 8m, "Nail Art" },
                    { 3, 8m, "Sticker and Gems" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Cost", "Type" },
                values: new object[,]
                {
                    { 1, 40m, "Manicure Gel" },
                    { 2, 40m, "Pedicure Gel" },
                    { 3, 55m, "SNS" },
                    { 4, 60m, "Acrylic" },
                    { 5, 50m, "BIAB" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddOns");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
