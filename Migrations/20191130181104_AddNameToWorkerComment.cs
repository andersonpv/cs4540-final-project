using Microsoft.EntityFrameworkCore.Migrations;

namespace cs4540_final_project.Migrations
{
    public partial class AddNameToWorkerComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "WorkerComment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "WorkerComment");
        }
    }
}
