using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleAccountScope_WithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleAccountScope",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountabilityTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                });

            migrationBuilder.InsertData(
                table: "RoleAccountScope",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2788), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2828), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToForntendInternalFOS", null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2930), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2932), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2912), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2914), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToForntendInternalTC", null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2953), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 2, 28, 12, 36, 34, 265, DateTimeKind.Unspecified).AddTicks(2954), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccountScope_AccountabilityTypeId",
                table: "RoleAccountScope",
                column: "AccountabilityTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAccountScope");
        }
    }
}
