using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SD7501_NailBookingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingToDBandSeedTable : Migration
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
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BookingOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Bookings",
                columns: new[] { "Id", "BookingOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Alice" },
                    { 2, 2, "Jasmine" },
                    { 3, 3, "Kelly" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "BookingId", "Cost", "Description", "ImageUrl", "Type" },
                values: new object[,]
                {
                    { 1, 1, 40m, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac.", "", "Manicure Gel" },
                    { 2, 1, 40m, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac.", "", "Pedicure Gel" },
                    { 3, 2, 55m, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac.", "", "SNS" },
                    { 4, 2, 60m, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac.", "", "Acrylic" },
                    { 5, 3, 50m, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac.", "", "BIAB" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_BookingId",
                table: "Services",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddOns");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
