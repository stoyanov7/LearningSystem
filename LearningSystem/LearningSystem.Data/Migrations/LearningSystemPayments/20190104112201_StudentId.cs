using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations.LearningSystemPayments
{
    public partial class StudentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Payments");
        }
    }
}
