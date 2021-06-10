using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealerApiService.App.Migrations
{
    public partial class RemoveSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 2042);

            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 3141);

            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 4022);

            migrationBuilder.DeleteData(
                table: "VehicleInventory",
                keyColumn: "Id",
                keyValue: 4041);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleInventory",
                columns: new[] { "Id", "Make", "MarketValue", "Model", "VinNumber", "Year" },
                values: new object[,]
                {
                    { 1050, "Tesla", 51, "T", "1233asd", 210 },
                    { 4022, "Tesla", 25, "A", "1223a2d", 2200 },
                    { 4041, "Tesla", 35, "B", "12333asd", 21200 },
                    { 1045, "Toyota", 45, "Camry", "123asd", 21010 },
                    { 3141, "Tesla", 55, "T", "12gg3a234sd", 52100 },
                    { 2021, "Car", 52, "a", "12d3asd", 21020 },
                    { 2042, "Ford", 15, "T", "123fasd", 25100 }
                });
        }
    }
}
