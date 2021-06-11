using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealerAPIService.App.Migrations
{
    public partial class AddedToVehicleSubmissionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSubmissions_VehicleInventory_VehicleId",
                table: "VehicleSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "VehicleSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Average = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Below = table.Column<long>(type: "bigint", nullable: false),
                    Above = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Average);
                });

            migrationBuilder.CreateTable(
                name: "MarketValues",
                columns: table => new
                {
                    Vin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Success = table.Column<bool>(type: "bit", nullable: false),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mileage = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    Mean = table.Column<long>(type: "bigint", nullable: false),
                    Stdev = table.Column<long>(type: "bigint", nullable: false),
                    Certainty = table.Column<long>(type: "bigint", nullable: false),
                    PricesAverage = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketValues", x => x.Vin);
                    table.ForeignKey(
                        name: "FK_MarketValues_Prices_PricesAverage",
                        column: x => x.PricesAverage,
                        principalTable: "Prices",
                        principalColumn: "Average",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketValues_PricesAverage",
                table: "MarketValues",
                column: "PricesAverage");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSubmissions_VehicleInventory_VehicleId",
                table: "VehicleSubmissions",
                column: "VehicleId",
                principalTable: "VehicleInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSubmissions_VehicleInventory_VehicleId",
                table: "VehicleSubmissions");

            migrationBuilder.DropTable(
                name: "MarketValues");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "VehicleSubmissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSubmissions_VehicleInventory_VehicleId",
                table: "VehicleSubmissions",
                column: "VehicleId",
                principalTable: "VehicleInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
