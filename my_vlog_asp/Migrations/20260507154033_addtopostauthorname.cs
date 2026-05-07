using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_vlog_asp.Migrations
{
    /// <inheritdoc />
    public partial class addtopostauthorname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Posts",
                newName: "theme");

            migrationBuilder.AddColumn<string>(
                name: "author_name",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "author_name",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "text",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "theme",
                table: "Posts",
                newName: "title");
        }
    }
}
