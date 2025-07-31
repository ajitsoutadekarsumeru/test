using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.Tenant.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlexTenant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HostName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TenantDbType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DefaultWriteDbConnectionString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DefaultReadDbConnectionString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsSharedTenant = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlexTenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantEmailConfiguration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Mailcc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MailFrom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailLogPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MailTo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MailSignature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmtpServer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmtpPort = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SmtpUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmtpPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EnableSsl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantEmailConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantEmailConfiguration_FlexTenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "FlexTenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantSMSConfiguration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSMSConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSMSConfiguration_FlexTenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "FlexTenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantEmailConfiguration_TenantId",
                table: "TenantEmailConfiguration",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSMSConfiguration_TenantId",
                table: "TenantSMSConfiguration",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantEmailConfiguration");

            migrationBuilder.DropTable(
                name: "TenantSMSConfiguration");

            migrationBuilder.DropTable(
                name: "FlexTenant");
        }
    }
}