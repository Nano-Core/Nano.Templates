using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nano.Template.Web.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Initial : Migration
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntitySetName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RequestId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sample",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sample", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuditProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RelationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "__EFAuthRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFAuthRoleClaim___EFAuthRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "__EFAuthRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFAuthUserClaim___EFAuthUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFAuthUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK___EFAuthUserLogin___EFAuthUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFAuthUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK___EFAuthUserRole___EFAuthRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "__EFAuthRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK___EFAuthUserRole___EFAuthUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFAuthUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__EFAuthUserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpireAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAuthUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK___EFAuthUserToken___EFAuthUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFAuthUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    IdentityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User___EFAuthUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "__EFAuthUser",
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

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "__EFAuthRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthRoleClaim_RoleId",
                table: "__EFAuthRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "__EFAuthUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthUser_Email",
                table: "__EFAuthUser",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthUser_PhoneNumber",
                table: "__EFAuthUser",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "__EFAuthUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthUserClaim_UserId",
                table: "__EFAuthUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthUserLogin_UserId",
                table: "__EFAuthUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuthUserRole_RoleId",
                table: "__EFAuthUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sample_CreatedAt",
                table: "Sample",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Sample_IsDeleted",
                table: "Sample",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sample_Name",
                table: "Sample",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedAt",
                table: "User",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_User_IsDeleted",
                table: "User",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "UX_User_IdentityUserId",
                table: "User",
                column: "IdentityUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_User_IdentityUserId_IsDeleted",
                table: "User",
                columns: new[] { "IdentityUserId", "IsDeleted" },
                unique: true);
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
                name: "__EFAuthRoleClaim");

            migrationBuilder.DropTable(
                name: "__EFAuthUserClaim");

            migrationBuilder.DropTable(
                name: "__EFAuthUserLogin");

            migrationBuilder.DropTable(
                name: "__EFAuthUserRole");

            migrationBuilder.DropTable(
                name: "__EFAuthUserToken");

            migrationBuilder.DropTable(
                name: "Sample");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "__EFAudit");

            migrationBuilder.DropTable(
                name: "__EFAuthRole");

            migrationBuilder.DropTable(
                name: "__EFAuthUser");
        }
    }
}
