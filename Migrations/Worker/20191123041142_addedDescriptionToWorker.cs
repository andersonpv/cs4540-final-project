using Microsoft.EntityFrameworkCore.Migrations;

namespace cs4540_final_project.Migrations.Worker
{
    public partial class addedDescriptionToWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Worker",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Worker");
        }
    }
}
