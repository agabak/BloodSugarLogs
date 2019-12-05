using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodSugarLog.Migrations
{
    public partial class updateForDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TakeTime",
                table: "FoodInTakes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakeTime",
                table: "FoodInTakes");
        }
    }
}
