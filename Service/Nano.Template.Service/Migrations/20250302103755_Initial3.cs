using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nano.Template.Service.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX___EFAuthApiKey_ExpireAt",
                table: "__EFAuthApiKey");

            migrationBuilder.DropColumn(
                name: "ExpireAt",
                table: "__EFAuthApiKey");

            migrationBuilder.AddColumn<string>(
                name: "JsonMapped",
                table: "Sample",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "__EFAuthUser",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "NewEmail",
                table: "__EFAuthUser",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NewPhoneNumber",
                table: "__EFAuthUser",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonMapped",
                table: "Sample");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "__EFAuthUser");

            migrationBuilder.DropColumn(
                name: "NewEmail",
                table: "__EFAuthUser");

            migrationBuilder.DropColumn(
                name: "NewPhoneNumber",
                table: "__EFAuthUser");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpireAt",
                table: "__EFAuthApiKey",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthApiKey_ExpireAt",
                table: "__EFAuthApiKey",
                column: "ExpireAt");
        }
    }
}
