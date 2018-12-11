using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations.LearningSystemPayments
{
    public partial class PaymentCourseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Payments");
        }
    }
}
