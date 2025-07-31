using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedBranchGeoMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerTemplate");

            migrationBuilder.DropColumn(
                name: "MaximumOccurences",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "MaxHotleadCount",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Address");

            migrationBuilder.UpdateData(
                table: "TriggerTemplateMapping",
                keyColumn: "CommunicationTriggerId",
                keyValue: null,
                column: "CommunicationTriggerId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationTriggerId",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "TriggerTemplateMapping",
                keyColumn: "CommunicationTemplateId",
                keyValue: null,
                column: "CommunicationTemplateId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationTemplateId",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "TriggerTemplateMapping",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TriggerTemplateMapping",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedDate",
                table: "TriggerTemplateMapping",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CommunicationTrigger",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableInAccountDetails",
                table: "CommunicationTemplate",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DPD_From",
                table: "Buckets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DPD_To",
                table: "Buckets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayLabel",
                table: "Buckets",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GeoLevelId",
                table: "ApplicationUser",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProductLevelId",
                table: "ApplicationUser",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "Address",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TriggerTemplateMapping",
                table: "TriggerTemplateMapping",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BranchGeoMap",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BranchId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HierarchyId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HierarchyLevel = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_BranchGeoMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchGeoMap_ApplicationOrg_BranchId",
                        column: x => x.BranchId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchGeoMap_HierarchyMaster_HierarchyId",
                        column: x => x.HierarchyId,
                        principalTable: "HierarchyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9550), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9584), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9599), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9601), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9592), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9594), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9605), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9607), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTemplateId",
                table: "TriggerTemplateMapping",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTriggerId",
                table: "TriggerTemplateMapping",
                column: "CommunicationTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_GeoLevelId",
                table: "ApplicationUser",
                column: "GeoLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ProductLevelId",
                table: "ApplicationUser",
                column: "ProductLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGeoMap_BranchId",
                table: "BranchGeoMap",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGeoMap_HierarchyId",
                table: "BranchGeoMap",
                column: "HierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_GeoLevelId",
                table: "ApplicationUser",
                column: "GeoLevelId",
                principalTable: "HierarchyLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_ProductLevelId",
                table: "ApplicationUser",
                column: "ProductLevelId",
                principalTable: "HierarchyLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_TriggerType_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId",
                principalTable: "TriggerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBucketScope_ApplicationUser_ApplicationUserId",
                table: "UserBucketScope",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBucketScope_Buckets_BucketScopeId",
                table: "UserBucketScope",
                column: "BucketScopeId",
                principalTable: "Buckets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_GeoLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_ProductLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_TriggerType_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBucketScope_ApplicationUser_ApplicationUserId",
                table: "UserBucketScope");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBucketScope_Buckets_BucketScopeId",
                table: "UserBucketScope");

            migrationBuilder.DropTable(
                name: "BranchGeoMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TriggerTemplateMapping",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTemplateId",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTriggerId",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_GeoLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_ProductLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "TriggerTemplateMapping");

            migrationBuilder.DropColumn(
                name: "IsAvailableInAccountDetails",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "DPD_From",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "DPD_To",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "DisplayLabel",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "GeoLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ProductLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationTriggerId",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationTemplateId",
                table: "TriggerTemplateMapping",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CommunicationTrigger",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CommunicationTrigger",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "MaximumOccurences",
                table: "CommunicationTrigger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaxHotleadCount",
                table: "ApplicationUser",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Address",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Address",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Address",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommunicationTemplateId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommunicationTriggerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTriggerTemplate", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9675), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9746), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9829), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9833), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9800), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9806), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9852), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9855), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTriggerTemplate_CommunicationTemplateId",
                table: "CommunicationTriggerTemplate",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTriggerTemplate_CommunicationTriggerId",
                table: "CommunicationTriggerTemplate",
                column: "CommunicationTriggerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
