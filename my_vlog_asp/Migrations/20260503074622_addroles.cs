using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_vlog_asp.Migrations
{
    /// <inheritdoc />
    public partial class addroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_Userid",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_Userid",
                table: "Post",
                newName: "IX_Post_Userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Users_Userid",
                table: "Post",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Users_Userid",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_Post_Userid",
                table: "Posts",
                newName: "IX_Posts_Userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_Userid",
                table: "Posts",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }
    }
}
