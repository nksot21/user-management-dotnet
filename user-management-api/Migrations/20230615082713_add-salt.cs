using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user_management_api.Migrations
{
    /// <inheritdoc />
    public partial class addsalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "salt",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "user");
        }
    }
}
