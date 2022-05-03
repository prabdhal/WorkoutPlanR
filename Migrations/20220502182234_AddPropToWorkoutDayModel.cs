using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutPlannerWebApp.Migrations
{
    public partial class AddPropToWorkoutDayModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "WorkoutDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkoutDays",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkoutDays");
        }
    }
}
