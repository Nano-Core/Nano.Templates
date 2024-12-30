using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nano.Template.Service.Migrations
{
    /// <inheritdoc />
    public partial class Intial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__EFAuthApiKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdentityUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ExpireAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    RevokedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthApiKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFAuthApiKey___EFAuthUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "__EFAuthUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthApiKey_ExpireAt",
                table: "__EFAuthApiKey",
                column: "ExpireAt");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthApiKey_IdentityUserId",
                table: "__EFAuthApiKey",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthApiKey_RevokedAt",
                table: "__EFAuthApiKey",
                column: "RevokedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFAuthApiKey");
        }
    }
}
