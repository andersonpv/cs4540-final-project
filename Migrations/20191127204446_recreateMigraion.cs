using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cs4540_final_project.Migrations
{
    public partial class recreateMigraion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Services = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Worker_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DaySchedule",
                columns: table => new
                {
                    DayScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateTime = table.Column<DateTime>(nullable: false),
                    Nine = table.Column<bool>(nullable: false),
                    NineThirty = table.Column<bool>(nullable: false),
                    Ten = table.Column<bool>(nullable: false),
                    TenThirty = table.Column<bool>(nullable: false),
                    Eleven = table.Column<bool>(nullable: false),
                    ElevenThirty = table.Column<bool>(nullable: false),
                    Twelve = table.Column<bool>(nullable: false),
                    TwelveThirty = table.Column<bool>(nullable: false),
                    One = table.Column<bool>(nullable: false),
                    OneThirty = table.Column<bool>(nullable: false),
                    Two = table.Column<bool>(nullable: false),
                    TwoThirty = table.Column<bool>(nullable: false),
                    Three = table.Column<bool>(nullable: false),
                    ThreeThirty = table.Column<bool>(nullable: false),
                    Four = table.Column<bool>(nullable: false),
                    FourThirty = table.Column<bool>(nullable: false),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchedule", x => x.DayScheduleID);
                    table.ForeignKey(
                        name: "FK_DaySchedule_Worker_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Worker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkerComment",
                columns: table => new
                {
                    WorkerCommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    StarRating = table.Column<int>(nullable: false),
                    WorkerID = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerComment", x => x.WorkerCommentID);
                    table.ForeignKey(
                        name: "FK_WorkerComment_Worker_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Worker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedule_WorkerID",
                table: "DaySchedule",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_UserId",
                table: "Worker",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerComment_WorkerID",
                table: "WorkerComment",
                column: "WorkerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaySchedule");

            migrationBuilder.DropTable(
                name: "WorkerComment");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
