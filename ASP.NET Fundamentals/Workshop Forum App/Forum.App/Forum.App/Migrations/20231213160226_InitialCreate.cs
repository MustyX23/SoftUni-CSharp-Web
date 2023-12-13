using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.App.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("157b4187-8b5d-4107-adb1-7ff6537eef69"), "Embark on a journey through the lush, green canopy of the forest. High above the forest floor, the intertwining branches create a natural tapestry, allowing you to traverse through a world where sunlight filters through a million leaves, painting the ground below in a dappled mosaic.", "Journey Through the Verdant Canopy" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("1f344c4a-2aa2-4745-b8d3-29d331bd36a8"), "The whispering pines stand tall, their needles holding the ancient wisdom of the forest. Each gust of wind carries tales of centuries past, tales of resilience and adaptation.", "Eternal Wisdom of the Whispering Pines" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("e23ac0d5-48cf-4e0d-8e59-32ff0c1aa9d8"), "Amidst the ancient trees, where emerald leaves dance with the breeze, a mystical grove comes alive. Listen closely, and you'll hear the whispers of the forest spirits and the secrets hidden within the rustling leaves.", "Whispers of the Enchanted Grove" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
