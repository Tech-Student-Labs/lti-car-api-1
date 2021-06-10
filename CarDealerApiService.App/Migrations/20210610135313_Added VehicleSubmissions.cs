using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealerApiService.App.Migrations
{
    public partial class AddedVehicleSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleSubmissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleSubmissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleSubmissions_VehicleInventory_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "VehicleInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSubmissions_UserId",
                table: "VehicleSubmissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSubmissions_VehicleId",
                table: "VehicleSubmissions",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleSubmissions");
        }
    }
}
