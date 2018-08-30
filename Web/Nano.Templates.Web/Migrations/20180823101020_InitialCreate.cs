using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nano.Templates.Web.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__EFAudit",
                columns: table => new
                {
                    IsDeleted = table.Column<long>(nullable: false, defaultValue: 0L),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    EntitySetName = table.Column<string>(maxLength: 255, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    State = table.Column<int>(nullable: false),
                    StateName = table.Column<string>(maxLength: 255, nullable: true),
                    RequestId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthRoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserLogin", x => new { x.UserId, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserRole", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserToken", x => new { x.UserId, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "__EFAuditProperties",
                columns: table => new
                {
                    IsDeleted = table.Column<long>(nullable: false, defaultValue: 0L),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    PropertyName = table.Column<string>(maxLength: 255, nullable: true),
                    RelationName = table.Column<string>(maxLength: 255, nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    OldValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuditProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFAuditProperties___EFAudit_ParentId",
                        column: x => x.ParentId,
                        principalTable: "__EFAudit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_CreatedAt",
                table: "__EFAudit",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_CreatedBy",
                table: "__EFAudit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_EntityTypeName",
                table: "__EFAudit",
                column: "EntityTypeName");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_IsDeleted",
                table: "__EFAudit",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_RequestId",
                table: "__EFAudit",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_State",
                table: "__EFAudit",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuditProperties_CreatedAt",
                table: "__EFAuditProperties",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuditProperties_IsDeleted",
                table: "__EFAuditProperties",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuditProperties_ParentId",
                table: "__EFAuditProperties",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuditProperties_PropertyName",
                table: "__EFAuditProperties",
                column: "PropertyName");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFAuditProperties");

            migrationBuilder.DropTable(
                name: "__EFAuthRole");

            migrationBuilder.DropTable(
                name: "__EFAuthRoleClaim");

            migrationBuilder.DropTable(
                name: "__EFAuthUser");

            migrationBuilder.DropTable(
                name: "__EFAuthUserClaim");

            migrationBuilder.DropTable(
                name: "__EFAuthUserLogin");

            migrationBuilder.DropTable(
                name: "__EFAuthUserRole");

            migrationBuilder.DropTable(
                name: "__EFAuthUserToken");

            migrationBuilder.DropTable(
                name: "__EFAudit");
        }
    }
}
