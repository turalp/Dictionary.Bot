using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dictionary.Domain.Migrations
{
    public partial class ChangePrimaryKeyInWordDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_WordsDescriptions_WordDescriptionId",
                table: "WordsDescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordsDescriptions",
                table: "WordsDescriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordsDescriptions",
                table: "WordsDescriptions",
                column: "WordDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsDescriptions_WordId",
                table: "WordsDescriptions",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WordsDescriptions",
                table: "WordsDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_WordsDescriptions_WordId",
                table: "WordsDescriptions");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_WordsDescriptions_WordDescriptionId",
                table: "WordsDescriptions",
                column: "WordDescriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordsDescriptions",
                table: "WordsDescriptions",
                columns: new[] { "WordId", "DescriptionId" });
        }
    }
}
