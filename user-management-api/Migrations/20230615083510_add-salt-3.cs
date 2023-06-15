using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user_management_api.Migrations
{
    /// <inheritdoc />
    public partial class addsalt3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "user",
                newName: "deletedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deletedAt",
                table: "user",
                newName: "DeletedAt");
        }
    }
}
