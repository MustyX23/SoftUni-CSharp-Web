using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.Migrations
{
    public partial class UserBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook");

            migrationBuilder.RenameTable(
                name: "IdentityUserBook",
                newName: "UserBooks");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserBook_CollectorId",
                table: "UserBooks",
                newName: "IX_UserBooks_CollectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBooks",
                table: "UserBooks",
                columns: new[] { "BookId", "CollectorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_CollectorId",
                table: "UserBooks",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_CollectorId",
                table: "UserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBooks",
                table: "UserBooks");

            migrationBuilder.RenameTable(
                name: "UserBooks",
                newName: "IdentityUserBook");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_CollectorId",
                table: "IdentityUserBook",
                newName: "IX_IdentityUserBook_CollectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook",
                columns: new[] { "BookId", "CollectorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
