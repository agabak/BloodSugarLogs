using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodSugarLog.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloodValue",
                table: "FoodInTakes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodValue",
                table: "FoodInTakes");
        }
    }
}
