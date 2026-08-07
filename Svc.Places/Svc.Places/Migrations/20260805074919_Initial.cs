using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Svc.Places.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFAudit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityKey = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EntitySetName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityTypeName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityState = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RequestId = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFAudit", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFDataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FriendlyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Xml = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFDataProtectionKeys", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityRole", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUser", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameNormalized = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(8192)", maxLength: 8192, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Area = table.Column<Polygon>(type: "polygon", nullable: false),
                    FavoriteCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    VisitsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LatestVisit = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FullName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFAuditProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PropertyName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RelationName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OldValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityRoleClaim___EFIdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "__EFIdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityApiKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdentityUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityApiKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityApiKey___EFIdentityUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUserChangeData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdentityUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NewEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewPhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUserChangeData", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityUserChangeData___EFIdentityUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityUserClaim___EFIdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK___EFIdentityUserLogin___EFIdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUserRefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdentityUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppId = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpireAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUserRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityUserRefreshToken___EFIdentityUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK___EFIdentityUserRole___EFIdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "__EFIdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK___EFIdentityUserRole___EFIdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityUserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK___EFIdentityUserToken___EFIdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "__EFIdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OpeningHour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlaceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OpensAt = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ClosesAt = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHour_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlacePicture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlaceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacePicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacePicture_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlaceFavorite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlaceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceFavorite_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceFavorite_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlaceVisit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlaceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EnteredAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LeftAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceVisit_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceVisit_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityApiKeyClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ApiKeyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClaimType = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityApiKeyClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityApiKeyClaim___EFIdentityApiKey_ApiKeyId",
                        column: x => x.ApiKeyId,
                        principalTable: "__EFIdentityApiKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__EFIdentityApiKeyRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ApiKeyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFIdentityApiKeyRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK___EFIdentityApiKeyRole___EFIdentityApiKey_ApiKeyId",
                        column: x => x.ApiKeyId,
                        principalTable: "__EFIdentityApiKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK___EFIdentityApiKeyRole___EFIdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "__EFIdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_CreatedBy",
                table: "__EFAudit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_EntityKey",
                table: "__EFAudit",
                column: "EntityKey");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_EntityState",
                table: "__EFAudit",
                column: "EntityState");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_EntityTypeName",
                table: "__EFAudit",
                column: "EntityTypeName");

            migrationBuilder.CreateIndex(
                name: "IX___EFAudit_RequestId",
                table: "__EFAudit",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuditProperties_ParentId",
                table: "__EFAuditProperties",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX___EFAuditProperties_PropertyName",
                table: "__EFAuditProperties",
                column: "PropertyName");

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityApiKey_IdentityUserId",
                table: "__EFIdentityApiKey",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityApiKey_RevokedAt",
                table: "__EFIdentityApiKey",
                column: "RevokedAt");

            migrationBuilder.CreateIndex(
                name: "UX___EFIdentityApiKeyClaim_ApiKeyId_ClaimType",
                table: "__EFIdentityApiKeyClaim",
                columns: new[] { "ApiKeyId", "ClaimType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityApiKeyRole_RoleId",
                table: "__EFIdentityApiKeyRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UX___EFIdentityApiKeyRole_ApiKeyId_RoleId",
                table: "__EFIdentityApiKeyRole",
                columns: new[] { "ApiKeyId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "__EFIdentityRole",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityRoleClaim_RoleId",
                table: "__EFIdentityRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "__EFIdentityUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUser_Email",
                table: "__EFIdentityUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUser_IsActive",
                table: "__EFIdentityUser",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUser_PhoneNumber",
                table: "__EFIdentityUser",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "__EFIdentityUser",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX___EFIdentityUserChangeData_IdentityUserId",
                table: "__EFIdentityUserChangeData",
                column: "IdentityUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUserClaim_UserId",
                table: "__EFIdentityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUserLogin_UserId",
                table: "__EFIdentityUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUserRefreshToken_ExpireAt",
                table: "__EFIdentityUserRefreshToken",
                column: "ExpireAt");

            migrationBuilder.CreateIndex(
                name: "UX___EFIdentityUserRefreshToken_IdentityUserId_AppId",
                table: "__EFIdentityUserRefreshToken",
                columns: new[] { "IdentityUserId", "AppId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___EFIdentityUserRole_RoleId",
                table: "__EFIdentityUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHour_CreatedAt",
                table: "OpeningHour",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHour_IsDeleted",
                table: "OpeningHour",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHour_PlaceId",
                table: "OpeningHour",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_Area",
                table: "Place",
                column: "Area")
                .Annotation("MySql:SpatialIndex", true);

            migrationBuilder.CreateIndex(
                name: "IX_Place_CreatedAt",
                table: "Place",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Place_IsDeleted",
                table: "Place",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Place_NameNormalized",
                table: "Place",
                column: "NameNormalized");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceFavorite_CreatedAt",
                table: "PlaceFavorite",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceFavorite_IsDeleted",
                table: "PlaceFavorite",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceFavorite_PlaceId",
                table: "PlaceFavorite",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceFavorite_UserId",
                table: "PlaceFavorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacePicture_CreatedAt",
                table: "PlacePicture",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlacePicture_IsDeleted",
                table: "PlacePicture",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_PlacePicture_PlaceId_OrderIndex",
                table: "PlacePicture",
                columns: new[] { "PlaceId", "OrderIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVisit_CreatedAt",
                table: "PlaceVisit",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVisit_IsDeleted",
                table: "PlaceVisit",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVisit_PlaceId",
                table: "PlaceVisit",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVisit_UserId_PlaceId_LeftAt",
                table: "PlaceVisit",
                columns: new[] { "UserId", "PlaceId", "LeftAt" });

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedAt",
                table: "User",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_User_IsActive",
                table: "User",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_User_IsDeleted",
                table: "User",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFAuditProperties");

            migrationBuilder.DropTable(
                name: "__EFDataProtectionKeys");

            migrationBuilder.DropTable(
                name: "__EFIdentityApiKeyClaim");

            migrationBuilder.DropTable(
                name: "__EFIdentityApiKeyRole");

            migrationBuilder.DropTable(
                name: "__EFIdentityRoleClaim");

            migrationBuilder.DropTable(
                name: "__EFIdentityUserChangeData");

            migrationBuilder.DropTable(
                name: "__EFIdentityUserClaim");

            migrationBuilder.DropTable(
                name: "__EFIdentityUserLogin");

            migrationBuilder.DropTable(
                name: "__EFIdentityUserRefreshToken");

            migrationBuilder.DropTable(
                name: "__EFIdentityUserRole");

            migrationBuilder.DropTable(
                name: "__EFIdentityUserToken");

            migrationBuilder.DropTable(
                name: "OpeningHour");

            migrationBuilder.DropTable(
                name: "PlaceFavorite");

            migrationBuilder.DropTable(
                name: "PlacePicture");

            migrationBuilder.DropTable(
                name: "PlaceVisit");

            migrationBuilder.DropTable(
                name: "__EFAudit");

            migrationBuilder.DropTable(
                name: "__EFIdentityApiKey");

            migrationBuilder.DropTable(
                name: "__EFIdentityRole");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "__EFIdentityUser");
        }
    }
}
