using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class AddEmailtoStudentParentandTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Parents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Parents");
        }
    }
}
