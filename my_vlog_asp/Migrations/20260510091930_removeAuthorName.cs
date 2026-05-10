using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_vlog_asp.Migrations
{
    /// <inheritdoc />
    public partial class removeAuthorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "author_name",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "author_name",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
