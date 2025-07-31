using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AlterDispositionCodeMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAccountScope");

            migrationBuilder.DropColumn(
                name: "DispositionCodeCustomerOrAccountLevel",
                table: "DispositionCodeMaster");

            migrationBuilder.AddColumn<bool>(
                name: "DispositionCodeIsCustomerLevel",
                table: "DispositionCodeMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AccountScopeConfiguration",
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
                    table.PrimaryKey("PK_AccountScopeConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountScopeConfiguration_AccountabilityTypes_AccountabilityTypeId",
                        column: x => x.AccountabilityTypeId,
                        principalTable: "AccountabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountScopeConfiguration",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5447), new TimeSpan(0, 2, 0, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5486), new TimeSpan(0, 2, 0, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5501), new TimeSpan(0, 2, 0, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5502), new TimeSpan(0, 2, 0, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5494), new TimeSpan(0, 2, 0, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5495), new TimeSpan(0, 2, 0, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5513), new TimeSpan(0, 2, 0, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5514), new TimeSpan(0, 2, 0, 0, 0)), "all", 1 }
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

            migrationBuilder.DropColumn(
                name: "DispositionCodeIsCustomerLevel",
                table: "DispositionCodeMaster");

            migrationBuilder.AddColumn<string>(
                name: "DispositionCodeCustomerOrAccountLevel",
                table: "DispositionCodeMaster",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleAccountScope",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountabilityTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.InsertData(
                table: "RoleAccountScope",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3272), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3330), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3378), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3355), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3360), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3425), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3429), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccountScope_AccountabilityTypeId",
                table: "RoleAccountScope",
                column: "AccountabilityTypeId");
        }
    }
}
