using Microsoft.EntityFrameworkCore.Migrations;

namespace Dictionary.Domain.Migrations
{
    public partial class ChangedUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Vcard",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "LanguageCode");

            migrationBuilder.AddColumn<bool>(
                name: "IsBot",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBot",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Vcard");

            migrationBuilder.RenameColumn(
                name: "LanguageCode",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }
    }
}
