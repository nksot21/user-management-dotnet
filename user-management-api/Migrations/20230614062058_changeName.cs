using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user_management_api.Migrations
{
    /// <inheritdoc />
    public partial class changeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_individualUsersModel",
                table: "individualUsersModel");

            migrationBuilder.RenameTable(
                name: "individualUsersModel",
                newName: "IndividualUsersModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualUsersModel",
                table: "IndividualUsersModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualUsersModel",
                table: "IndividualUsersModel");

            migrationBuilder.RenameTable(
                name: "IndividualUsersModel",
                newName: "individualUsersModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_individualUsersModel",
                table: "individualUsersModel",
                column: "Id");
        }
    }
}
