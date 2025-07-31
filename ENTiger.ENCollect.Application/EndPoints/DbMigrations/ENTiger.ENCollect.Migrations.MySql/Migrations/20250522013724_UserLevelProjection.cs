using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class UserLevelProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLevelProjection",
                table: "UserLevelProjection");

            migrationBuilder.RenameColumn(
                name: "MaxLevel",
                table: "UserLevelProjection",
                newName: "Level");

            migrationBuilder.UpdateData(
                table: "UserLevelProjection",
                keyColumn: "Id",
                keyValue: null,
                column: "Id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserLevelProjection",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLevelProjection",
                table: "UserLevelProjection",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevelProjection_ApplicationUserId",
                table: "UserLevelProjection",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLevelProjection",
                table: "UserLevelProjection");

            migrationBuilder.DropIndex(
                name: "IX_UserLevelProjection_ApplicationUserId",
                table: "UserLevelProjection");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "UserLevelProjection",
                newName: "MaxLevel");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserLevelProjection",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLevelProjection",
                table: "UserLevelProjection",
                column: "ApplicationUserId");
        }
    }
}
