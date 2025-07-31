using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountScopeConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAccountScope");

            migrationBuilder.CreateTable(
                name: "AccountScopeConfiguration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountabilityTypeId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Scope = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ScopeLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountScopeConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountScopeConfiguration_AccountabilityTypes_Accountability~",
                        column: x => x.AccountabilityTypeId,
                        principalTable: "AccountabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AccountScopeConfiguration",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7249), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7284), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7309), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7311), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7300), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7302), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7317), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7319), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountScopeConfiguration_AccountabilityTypeId",
                table: "AccountScopeConfiguration",
                column: "AccountabilityTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountScopeConfiguration");

            migrationBuilder.CreateTable(
                name: "RoleAccountScope",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountabilityTypeId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Scope = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ScopeLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAccountScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAccountScope_AccountabilityTypes_AccountabilityTypeId",
                        column: x => x.AccountabilityTypeId,
                        principalTable: "AccountabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "RoleAccountScope",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9815), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9858), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9886), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9889), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9875), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9877), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9898), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9901), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccountScope_AccountabilityTypeId",
                table: "RoleAccountScope",
                column: "AccountabilityTypeId");
        }
    }
}
