using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Api.Migrations
{
    public partial class localization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publishers",
                newName: "NameUz");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "NameUz");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Publishers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "Publishers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Books",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "Books",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "NameUz",
                table: "Publishers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameUz",
                table: "Books",
                newName: "Title");
        }
    }
}
