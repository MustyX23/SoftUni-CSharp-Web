using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoard.App.Data.Migrations
{
    public partial class TasksAndBoardsSeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Done" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { new Guid("01fc972d-e894-4e16-b23e-88e202c37d72"), 2, new DateTime(2023, 12, 20, 20, 9, 14, 432, DateTimeKind.Utc).AddTicks(3487), "Implementing better styling for all public pages", "7b759171-08e8-4c14-ad92-c364efd9ef32", "Improve CSS styles" },
                    { new Guid("60889497-53c2-4906-a791-8225d0f18d9c"), 2, new DateTime(2023, 10, 20, 20, 9, 14, 432, DateTimeKind.Utc).AddTicks(3503), "Create Desktop Client App for the RESTful TaskBoard service", "7b759171-08e8-4c14-ad92-c364efd9ef32", "Desktop Client App" },
                    { new Guid("6132018b-70af-4273-9279-ff4ea757eb00"), 1, new DateTime(2023, 12, 19, 20, 9, 14, 432, DateTimeKind.Utc).AddTicks(3506), "Create Tasks Client App for the RESTful TaskBoard service", "430f9c7c-2e27-40b8-bdff-8e54bddb2223", "Create Tasks" },
                    { new Guid("f2ef3854-cdb6-4130-bb75-4131077184c4"), 1, new DateTime(2023, 7, 20, 20, 9, 14, 432, DateTimeKind.Utc).AddTicks(3496), "Create Android Client App for the RESTful TaskBoard service", "430f9c7c-2e27-40b8-bdff-8e54bddb2223", "Android Client App" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
