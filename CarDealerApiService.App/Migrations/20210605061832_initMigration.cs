using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealerApiService.App.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    VinNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInventory", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleInventory");
        }
    }
}
