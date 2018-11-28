using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations
{
    public partial class CourseSeoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "CourseInstances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "CourseInstances");
        }
    }
}
