using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dictionary.Domain.Migrations
{
    public partial class AddWordsDescriptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_Words_WordId",
                table: "Descriptions");

            migrationBuilder.DropIndex(
                name: "IX_Descriptions_WordId",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "Descriptions");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    TelegramUserId = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Vcard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.TelegramUserId);
                });

            migrationBuilder.CreateTable(
                name: "WordsDescriptions",
                columns: table => new
                {
                    WordId = table.Column<Guid>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false),
                    WordDescriptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsDescriptions", x => new { x.WordId, x.DescriptionId });
                    table.UniqueConstraint("AK_WordsDescriptions_WordDescriptionId", x => x.WordDescriptionId);
                    table.ForeignKey(
                        name: "FK_WordsDescriptions_Descriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Descriptions",
                        principalColumn: "DescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordsDescriptions_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordsDescriptions_DescriptionId",
                table: "WordsDescriptions",
                column: "DescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WordsDescriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "WordId",
                table: "Descriptions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_WordId",
                table: "Descriptions",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_Words_WordId",
                table: "Descriptions",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "WordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
