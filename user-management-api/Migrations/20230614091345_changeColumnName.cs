using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user_management_api.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualUsersModel",
                table: "IndividualUsersModel");

            migrationBuilder.RenameTable(
                name: "IndividualUsersModel",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "user",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "user",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "user",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "user",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "fullname",
                table: "user",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "user",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "IndividualUsersModel");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "IndividualUsersModel",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "IndividualUsersModel",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "IndividualUsersModel",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "IndividualUsersModel",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "IndividualUsersModel",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "IndividualUsersModel",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "IndividualUsersModel",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "IndividualUsersModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "IndividualUsersModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fullname",
                table: "IndividualUsersModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "IndividualUsersModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualUsersModel",
                table: "IndividualUsersModel",
                column: "Id");
        }
    }
}
