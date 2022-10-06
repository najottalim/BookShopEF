using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Api.Migrations
{
    public partial class seedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedAt", "NameEn", "NameRu", "NameUz", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 6, 4, 27, 34, 676, DateTimeKind.Utc).AddTicks(6290), "Darakchi", "Даракчи", "Darakchi", 0, null },
                    { 2, new DateTime(2022, 10, 6, 4, 27, 34, 676, DateTimeKind.Utc).AddTicks(6300), "Folk word", "Слова народа", "Xaql so'zi", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "Password", "Phone", "State", "UpdatedAt", "UserRole" },
                values: new object[] { 1, new DateTime(2022, 10, 6, 4, 27, 34, 676, DateTimeKind.Utc).AddTicks(6200), "Jon", "Doe", "string", "991234567", 0, null, 2 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedAt", "Genre", "Isbn", "Language", "NameEn", "NameRu", "NameUz", "NumberOfPages", "Price", "PublishYear", "PublisherId", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 6, 4, 27, 34, 676, DateTimeKind.Utc).AddTicks(6380), 3, new Guid("59bd30d0-4fdf-4d43-a810-10380c5fdc92"), 0, "Barn", "Сарай", "Molxona", 200, 27000, 2020, 1, 0, null },
                    { 2, new DateTime(2022, 10, 6, 4, 27, 34, 676, DateTimeKind.Utc).AddTicks(6390), 4, new Guid("5e473171-6c78-4030-b28c-afca5ff7d23d"), 1, "Old bird", "Старая птичка", "Choli qushi", 240, 60000, 2021, 2, 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
