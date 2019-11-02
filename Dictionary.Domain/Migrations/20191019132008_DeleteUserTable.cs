using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dictionary.Domain.Migrations
{
    public partial class DeleteUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    TelegramUserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IsBot = table.Column<bool>(nullable: false),
                    LanguageCode = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.TelegramUserId);
                });
        }
    }
}
