using Microsoft.EntityFrameworkCore.Migrations;

namespace EventScheduler.Data.Migrations
{
    public partial class Color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "CalendarEvents",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "CalendarEvents");
        }
    }
}
