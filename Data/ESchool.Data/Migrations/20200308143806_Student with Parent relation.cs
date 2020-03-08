using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class StudentwithParentrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Classrooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParentsStudents",
                columns: table => new
                {
                    ParentId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentsStudents", x => new { x.StudentId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_ParentsStudents_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParentsStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentsStudents_ParentId",
                table: "ParentsStudents",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Teachers_TeacherId",
                table: "Classrooms",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Teachers_TeacherId",
                table: "Classrooms");

            migrationBuilder.DropTable(
                name: "ParentsStudents");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Classrooms");
        }
    }
}
