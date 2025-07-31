using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountabilityTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountabilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountLabels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LandMark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MainType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyUserWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUserWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllocationDownload",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    InputJson = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationDownload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buckets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buckets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BulkTrailUploadFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FileUploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MD5Hash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkTrailUploadFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BulkUploadFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FileUploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusFileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StatusFilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MD5Hash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsUploadstatus = table.Column<bool>(type: "bit", nullable: true),
                    RowsError = table.Column<int>(type: "int", nullable: true),
                    RowsProcessed = table.Column<int>(type: "int", nullable: true),
                    RowsSuccess = table.Column<int>(type: "int", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AllocationType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkUploadFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cash",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cash", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cheques",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InstrumentNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InstrumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MICRCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IFSCCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionBatchWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionBatchWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTemplateDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplateDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTemplateWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplateWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Culture",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepositBankMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DepositBankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DepositBranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DepositAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositBankMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignationType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    OldIMEI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IMEI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTPTimeStamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OTPCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DispositionGroupMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SrNo = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionAccess = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispositionGroupMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlexBusinessContext",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlexBusinessContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdConfigMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseValue = table.Column<int>(type: "int", nullable: false),
                    LatestValue = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdConfigMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdConfigMaster_SeedData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdConfigMaster_SeedData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterFileStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FileProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileUploadedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UploadType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFileStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Etc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultilingualEntitySet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultilingualEntitySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayInSlipWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayInSlipWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentGateways",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MerchantId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MerchantKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    APIKey = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ChecksumKey = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PostURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReturnURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServerToServerURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ErrorURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CancelURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentGateways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PinCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryAllocationFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FileUploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryAllocationFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryUnAllocationFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryUnAllocationFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProcessType = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryAllocationFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileUploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryAllocationFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryUnAllocationFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryUnAllocationFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SegmentationAdvanceFilter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BOM_POS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CHARGEOFF_DATE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CURRENT_POS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LOAN_AMOUNT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NONSTARTER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NPA_STAGEID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PRINCIPAL_OD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TOS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastDispositionCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastPaymentDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DispCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PTPDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerPersona = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CurrentDPD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditBureauScore = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerBehaviourScore1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerBehaviourScore2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EarlyWarningScore = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LegalStage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RepoStage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SettlementStage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerBehaviorScoreToKeepHisWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PreferredModeOfPayment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PropensityToPayOnline = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DigitalContactabilityScore = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CallContactabilityScore = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FieldContactabilityScore = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latest_Status_Of_SMS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latest_Status_Of_WhatsUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatementDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalOverdueAmount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DNDFlag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MinimumAmountDue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Month = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Year = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LOAN_STATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMI_OD_AMT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentationAdvanceFilter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SegmentationAdvanceFilterMasters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FieldId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentationAdvanceFilterMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sequence",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TFlexIdentificationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFlexIdentificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusToStop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExecutionStartdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionEnddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentHistoryDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    bucket = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    agreementid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    customername = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    allocationownerid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationOwnerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    telecallingagencyid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TCallingAgencyName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    current_dpd = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    telecallerid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    agencyid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    collectorid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgentName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    treatmentid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TreatmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SegmentationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TCallingAgentName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    treatmenthistoryid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    npa_stageid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    productgroup = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    latestmobileno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    state = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    zone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    segmentationid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bom_pos = table.Column<double>(type: "float", nullable: true),
                    current_pos = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    current_bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    loan_amount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    tos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dispcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    branch = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    city = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    product = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    subproduct = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    principal_od = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    customerid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentHistoryDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentUpdateIntermediate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AgreementId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationOwnerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TCAgencyId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TellecallerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CollectorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TreatmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkRequestId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentUpdateIntermediate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCurrentLocationDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCurrentLocationDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginKeys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPerformanceBandMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPerformanceBandMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPersonaMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPersonaMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersUpdateFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersUpdateFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserVerificationCodeTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVerificationCodeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accountabilities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CommisionerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ResponsibleId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountabilityTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accountabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accountabilities_AccountabilityTypes_AccountabilityTypeId",
                        column: x => x.AccountabilityTypeId,
                        principalTable: "AccountabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AadharNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AddressId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDetails_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankBranch",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MICR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankBranch_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CategoryMasterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryItem_CategoryItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CategoryItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryItem_CategoryMaster_CategoryMasterId",
                        column: x => x.CategoryMasterId,
                        principalTable: "CategoryMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TemplateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommunicationTemplateDetailId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CommunicationTemplateWorkflowStateId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    WATemplateId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationTemplate_CommunicationTemplateDetail_CommunicationTemplateDetailId",
                        column: x => x.CommunicationTemplateDetailId,
                        principalTable: "CommunicationTemplateDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommunicationTemplate_CommunicationTemplateWorkflowState_CommunicationTemplateWorkflowStateId",
                        column: x => x.CommunicationTemplateWorkflowStateId,
                        principalTable: "CommunicationTemplateWorkflowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Acronym = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartmentTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_DepartmentType_DepartmentTypeId",
                        column: x => x.DepartmentTypeId,
                        principalTable: "DepartmentType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DispositionCodeMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DispositionGroupMasterId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    SrNo = table.Column<long>(type: "bigint", nullable: false),
                    DispositionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permissibleforfieldagent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionAccess = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispositionCodeMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGroupMasterId",
                        column: x => x.DispositionGroupMasterId,
                        principalTable: "DispositionGroupMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlexDynamicBusinessRuleSequences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FlexBusinessContextId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PluginId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlexDynamicBusinessRuleSequences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlexDynamicBusinessRuleSequences_FlexBusinessContext_FlexBusinessContextId",
                        column: x => x.FlexBusinessContextId,
                        principalTable: "FlexBusinessContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubMenuMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SubMenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    hasAccess = table.Column<bool>(type: "bit", nullable: false),
                    MenuMasterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenuMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenuMaster_MenuMaster_MenuMasterId",
                        column: x => x.MenuMasterId,
                        principalTable: "MenuMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MultilingualEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MultilingualEntitySetId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CultureId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultilingualEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultilingualEntity_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Culture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultilingualEntity_MultilingualEntitySet_MultilingualEntitySetId",
                        column: x => x.MultilingualEntitySetId,
                        principalTable: "MultilingualEntitySet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Segmentation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExecutionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Product = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BOM_Bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrentBucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NPA_Flag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Current_DPD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    SegmentAdvanceFilterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ClusterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segmentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segmentation_SegmentationAdvanceFilter_SegmentAdvanceFilterId",
                        column: x => x.SegmentAdvanceFilterId,
                        principalTable: "SegmentationAdvanceFilter",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TFlexIdentificationDocTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TFlexIdentificationTypeId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFlexIdentificationDocTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TFlexIdentificationDocTypes_TFlexIdentificationTypes_TFlexIdentificationTypeId",
                        column: x => x.TFlexIdentificationTypeId,
                        principalTable: "TFlexIdentificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubTreatment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    TreatmentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AllocationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartDay = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EndDay = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ScriptToPersueCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualifyingCondition = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PreSubtreatmentOrder = table.Column<int>(type: "int", nullable: true),
                    QualifyingStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTreatment_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    NoOfAccounts = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LatestStamping = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SubTreatmentOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentHistory_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserVerificationCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ShortVerificationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    UserVerificationCodeTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TransactionID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVerificationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVerificationCodes_UserVerificationCodeTypes_UserVerificationCodeTypeId",
                        column: x => x.UserVerificationCodeTypeId,
                        principalTable: "UserVerificationCodeTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreditAccountDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountHolderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankBranchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditAccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditAccountDetails_BankBranch_BankBranchId",
                        column: x => x.BankBranchId,
                        principalTable: "BankBranch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimaryLanguage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondaryLanguage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Acronym = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DesignationTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ReportsToDesignation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Designation_DesignationType_DesignationTypeId",
                        column: x => x.DesignationTypeId,
                        principalTable: "DesignationType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DispositionValidationMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DispositionCodeMasterId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    validationFieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispositionValidationMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DispositionValidationMaster_DispositionCodeMaster_DispositionCodeMasterId",
                        column: x => x.DispositionCodeMasterId,
                        principalTable: "DispositionCodeMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HasAccess = table.Column<bool>(type: "bit", nullable: false),
                    SubMenuMasterID = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionMaster_SubMenuMaster_SubMenuMasterID",
                        column: x => x.SubMenuMasterID,
                        principalTable: "SubMenuMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentAndSegmentMapping",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SegmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentAndSegmentMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentAndSegmentMapping_Segmentation_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segmentation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentAndSegmentMapping_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoundRobinTreatment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AllocationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundRobinTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundRobinTreatment_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoundRobinTreatment_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOnCommunication",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CommunicationType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CommunicationTemplateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CommunicationMobileNumberType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOnCommunication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunication_CommunicationTemplate_CommunicationTemplateId",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunication_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunication_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOnPerformanceBand",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    PerformanceBand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CustomerPersona = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Percentage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOnPerformanceBand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOnPerformanceBand_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnPerformanceBand_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOnUpdateTrail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DispositionCodeGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DispositionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NextActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PTPAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOnUpdateTrail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOnUpdateTrail_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnUpdateTrail_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentQualifyingStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentQualifyingStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentQualifyingStatus_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentQualifyingStatus_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentByRule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Rule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentByRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentByRule_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentByRule_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentByRule_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentByRule_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentDesignation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentDesignation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentDesignation_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentDesignation_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentDesignation_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOnAccount",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Percentage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOnAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOnAccount_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnAccount_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnAccount_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnAccount_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOnPOS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Percentage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOnPOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOnPOS_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnPOS_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnPOS_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnPOS_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccessRights",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BusinessEntity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountabilityTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountabilityAs = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    MethodType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Route = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MenuMasterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ActionMasterId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    SubMenuMasterId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    IsMobile = table.Column<bool>(type: "bit", nullable: true),
                    IsFrontEnd = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRights_AccountabilityTypes_AccountabilityTypeId",
                        column: x => x.AccountabilityTypeId,
                        principalTable: "AccountabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRights_ActionMaster_ActionMasterId",
                        column: x => x.ActionMasterId,
                        principalTable: "ActionMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRights_MenuMaster_MenuMasterId",
                        column: x => x.MenuMasterId,
                        principalTable: "MenuMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRights_SubMenuMaster_SubMenuMasterId",
                        column: x => x.SubMenuMasterId,
                        principalTable: "SubMenuMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyIdentification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TFlexIdentificationTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TFlexIdentificationDocTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeferred = table.Column<bool>(type: "bit", nullable: true),
                    DeferredTillDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWavedOff = table.Column<bool>(type: "bit", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyIdentification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyIdentification_TFlexIdentificationDocTypes_TFlexIdentificationDocTypeId",
                        column: x => x.TFlexIdentificationDocTypeId,
                        principalTable: "TFlexIdentificationDocTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyIdentification_TFlexIdentificationTypes_TFlexIdentificationTypeId",
                        column: x => x.TFlexIdentificationTypeId,
                        principalTable: "TFlexIdentificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyIdentificationDoc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TFlexIdentificationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyIdentificationDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyIdentificationDoc_AgencyIdentification_TFlexIdentificationId",
                        column: x => x.TFlexIdentificationId,
                        principalTable: "AgencyIdentification",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgencyPlaceOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyPlaceOfWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyScopeOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyScopeOfWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyUserDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AgencyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUserDesignation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyUserDesignation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgencyUserDesignation_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgencyUserIdentification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TFlexIdentificationTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TFlexIdentificationDocTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsDeferred = table.Column<bool>(type: "bit", nullable: true),
                    DeferredTillDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWavedOff = table.Column<bool>(type: "bit", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUserIdentification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyUserIdentification_TFlexIdentificationDocTypes_TFlexIdentificationDocTypeId",
                        column: x => x.TFlexIdentificationDocTypeId,
                        principalTable: "TFlexIdentificationDocTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyUserIdentification_TFlexIdentificationTypes_TFlexIdentificationTypeId",
                        column: x => x.TFlexIdentificationTypeId,
                        principalTable: "TFlexIdentificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyUserIdentificationDoc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TFlexIdentificationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUserIdentificationDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyUserIdentificationDoc_AgencyUserIdentification_TFlexIdentificationId",
                        column: x => x.TFlexIdentificationId,
                        principalTable: "AgencyUserIdentification",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgencyUserPlaceOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUserPlaceOfWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyUserScopeOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Experience = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyUserScopeOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyUserScopeOfWork_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgencyUserScopeOfWork_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationOrg",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsBlackListed = table.Column<bool>(type: "bit", nullable: true),
                    BlackListingReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContractExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstAgreementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceTaxRegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeactivationReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsParentAgency = table.Column<bool>(type: "bit", nullable: true),
                    PAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GSTIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryOwnerFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryOwnerLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryContactCountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryContactAreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryContactCountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryContactAreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YardAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumberOfYards = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    YardSize = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ParentAgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    RecommendingOfficerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreditAccountDetailsId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyCategoryId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AddressId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyWorkflowStateId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    BankSolID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryMobileNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryContactNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PrimaryEMail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActivationCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    isOrganization = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_AgencyCategory_AgencyCategoryId",
                        column: x => x.AgencyCategoryId,
                        principalTable: "AgencyCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_AgencyType_AgencyTypeId",
                        column: x => x.AgencyTypeId,
                        principalTable: "AgencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_AgencyWorkflowState_AgencyWorkflowStateId",
                        column: x => x.AgencyWorkflowStateId,
                        principalTable: "AgencyWorkflowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_ApplicationOrg_ParentAgencyId",
                        column: x => x.ParentAgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_CreditAccountDetails_CreditAccountDetailsId",
                        column: x => x.CreditAccountDetailsId,
                        principalTable: "CreditAccountDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOrg_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeactivationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    IsBlackListed = table.Column<bool>(type: "bit", nullable: false),
                    BlackListingReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ForgotPasswordMailSentCount = table.Column<int>(type: "int", nullable: false),
                    ForgotPasswordSMSSentCount = table.Column<int>(type: "int", nullable: false),
                    ForgotPasswordCount = table.Column<int>(type: "int", nullable: false),
                    ForgotPasswordDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateMobileDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateMobileCount = table.Column<int>(type: "int", nullable: false),
                    ProductGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Product = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaxHotleadCount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkOfficeLongitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkOfficeLattitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryContactCountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserLoad = table.Column<int>(type: "int", nullable: true),
                    Experience = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserCurrentLocationDetailsId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreditAccountDetailsId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ApplicationUserDetailsId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    IdCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmploymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorizationCardExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SupervisorEmailId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryContactAreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiallerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UDIDNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DisableReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DeactivateReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: true),
                    yCoreBankingId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DRACertificateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DRATrainingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DRAUniqueRegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyUserWorkflowStateId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    IsFrontEndStaff = table.Column<bool>(type: "bit", nullable: true),
                    DomainId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyUser_PrimaryContactAreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SinglePointReportingManagerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    BaseBranchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TrailCap = table.Column<int>(type: "int", nullable: true),
                    CompanyUserWorkflowStateId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimaryMobileNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryContactNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PrimaryEMail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActivationCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    isOrganization = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_AgencyUserWorkflowState_AgencyUserWorkflowStateId",
                        column: x => x.AgencyUserWorkflowStateId,
                        principalTable: "AgencyUserWorkflowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationOrg_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationOrg_BaseBranchId",
                        column: x => x.BaseBranchId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationUserDetails_ApplicationUserDetailsId",
                        column: x => x.ApplicationUserDetailsId,
                        principalTable: "ApplicationUserDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationUser_SinglePointReportingManagerId",
                        column: x => x.SinglePointReportingManagerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_CompanyUserWorkflowState_CompanyUserWorkflowStateId",
                        column: x => x.CompanyUserWorkflowStateId,
                        principalTable: "CompanyUserWorkflowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_CreditAccountDetails_CreditAccountDetailsId",
                        column: x => x.CreditAccountDetailsId,
                        principalTable: "CreditAccountDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_UserCurrentLocationDetails_UserCurrentLocationDetailsId",
                        column: x => x.UserCurrentLocationDetailsId,
                        principalTable: "UserCurrentLocationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BaseBranchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_ApplicationOrg_BaseBranchId",
                        column: x => x.BaseBranchId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Areas_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseBranchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoMaster_ApplicationOrg_BaseBranchId",
                        column: x => x.BaseBranchId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserARMScopeOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SupervisingManagerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ReportingAgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserARMScopeOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserARMScopeOfWork_ApplicationOrg_ReportingAgencyId",
                        column: x => x.ReportingAgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserARMScopeOfWork_ApplicationUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserARMScopeOfWork_ApplicationUser_SupervisingManagerId",
                        column: x => x.SupervisingManagerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CompanyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserDesignation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserDesignation_ApplicationUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserDesignation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserDesignation_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserPlaceOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserPlaceOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserPlaceOfWork_ApplicationUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserScopeOfWork",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bucket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupervisingManagerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CompanyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserScopeOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserScopeOfWork_ApplicationUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserScopeOfWork_ApplicationUser_SupervisingManagerId",
                        column: x => x.SupervisingManagerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserScopeOfWork_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUserScopeOfWork_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeoTagDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    GeoTagReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    GeoLocation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoTagDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoTagDetails_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AGREEMENTID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BRANCH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CUSTOMERID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CUSTOMERNAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DispCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PRODUCT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PTPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LatestMobileNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LatestEmailId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LatestLatitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LatestLongitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LatestPTPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LatestPTPAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    LatestPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LatestFeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LatestFeedbackId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LenderId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CustomerPersona = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDNDEnabled = table.Column<bool>(type: "bit", nullable: false),
                    BOM_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BUCKET = table.Column<long>(type: "bigint", nullable: true),
                    CITY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CURRENT_BUCKET = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllocationOwnerExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CURRENT_DPD = table.Column<long>(type: "bigint", nullable: true),
                    CURRENT_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DISBURSEDAMOUNT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMI_OD_AMT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EMI_START_DATE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMIAMT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    INTEREST_OD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MAILINGMOBILE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MAILINGZIPCODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MONTH = table.Column<int>(type: "int", nullable: true),
                    NO_OF_EMI_OD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NPA_STAGEID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PENAL_PENDING = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PRINCIPAL_OD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    REGDNUM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    STATE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PAYMENTSTATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YEAR = table.Column<int>(type: "int", nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TOS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TOTAL_ARREARS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OVERDUE_DATE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NEXT_DUE_DATE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Excess = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LOAN_STATUS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OTHER_CHARGES = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TOTAL_OUTSTANDING = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OVERDUE_DAYS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeleCallingAgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TeleCallerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AllocationOwnerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyAllocationExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeleCallerAgencyAllocationExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AgentAllocationExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectorAllocationExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeleCallerAllocationExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CO_APPLICANT1_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NEXT_DUE_AMOUNT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Paid = table.Column<int>(type: "int", nullable: true),
                    Attempted = table.Column<int>(type: "int", nullable: true),
                    UnAttempted = table.Column<int>(type: "int", nullable: true),
                    Partner_Loan_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsEligibleForSettlement = table.Column<bool>(type: "bit", nullable: true),
                    IsEligibleForRepossession = table.Column<bool>(type: "bit", nullable: true),
                    IsEligibleForLegal = table.Column<bool>(type: "bit", nullable: true),
                    IsEligibleForRestructure = table.Column<bool>(type: "bit", nullable: true),
                    EMAIL_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PAN_CARD_DETAILS = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    SCHEME_DESC = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ZONE = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CentreID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CentreName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PRIMARY_CARD_NUMBER = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    BILLING_CYCLE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    LAST_STATEMENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CURRENT_MINIMUM_AMOUNT_DUE = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    CURRENT_TOTAL_AMOUNT_DUE = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    RESIDENTIAL_CUSTOMER_CITY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RESIDENTIAL_CUSTOMER_STATE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RESIDENTIAL_PIN_CODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RESIDENTIAL_COUNTRY = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanAccounts_ApplicationOrg_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccounts_ApplicationOrg_TeleCallingAgencyId",
                        column: x => x.TeleCallingAgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccounts_ApplicationUser_AllocationOwnerId",
                        column: x => x.AllocationOwnerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccounts_ApplicationUser_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccounts_ApplicationUser_TeleCallerId",
                        column: x => x.TeleCallerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccounts_CategoryItem_LenderId",
                        column: x => x.LenderId,
                        principalTable: "CategoryItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanAccounts_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayInSlips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CMSPayInSlipNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfDeposit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPrintValid = table.Column<bool>(type: "bit", maxLength: 50, nullable: true),
                    PrintedById = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    PrintedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Lattitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PayInSlipImageName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PayinslipType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayInSlipWorkflowStateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayInSlips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayInSlips_ApplicationUser_PrintedById",
                        column: x => x.PrintedById,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayInSlips_PayInSlipWorkflowState_PayInSlipWorkflowStateId",
                        column: x => x.PayInSlipWorkflowStateId,
                        principalTable: "PayInSlipWorkflowState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CollectorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptWorkflowStateId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_ApplicationUser_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_ReceiptWorkflowState_ReceiptWorkflowStateId",
                        column: x => x.ReceiptWorkflowStateId,
                        principalTable: "ReceiptWorkflowState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAttendanceDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TotalHours = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAttendanceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAttendanceDetail_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAttendanceLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSessionValid = table.Column<bool>(type: "bit", nullable: false),
                    LogOutLongitude = table.Column<double>(type: "float", nullable: true),
                    LogInLongitude = table.Column<double>(type: "float", nullable: true),
                    LogOutLatitude = table.Column<double>(type: "float", nullable: true),
                    LogInLatitude = table.Column<double>(type: "float", nullable: true),
                    LogInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAttendanceLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAttendanceLog_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCustomerPersona",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCustomerPersona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCustomerPersona_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPerformanceBand",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AgencyUserId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPerformanceBand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPerformanceBand_ApplicationUser_AgencyUserId",
                        column: x => x.AgencyUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPerformanceBand_ApplicationUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSearchCriteria",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    isdisable = table.Column<bool>(type: "bit", nullable: true),
                    FilterValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UseCaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSearchCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSearchCriteria_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreaBaseBranchMappings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AreaId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BaseBranchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaBaseBranchMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaBaseBranchMappings_ApplicationOrg_BaseBranchId",
                        column: x => x.BaseBranchId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaBaseBranchMappings_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreaPinCodeMappings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AreaId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    PinCodeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaPinCodeMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaPinCodeMappings_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AreaPinCodeMappings_PinCodes_PinCodeId",
                        column: x => x.PinCodeId,
                        principalTable: "PinCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UploadedFileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerMet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DispositionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DispositionGroup = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PTPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EscalateTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReallocationRequest = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReallocationRequestReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NewArea = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NewAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NewContactNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RightPartyContact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NextAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NonPaymentReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AssignReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NewContactCountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NewContactAreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NewEmailId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PickAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OtherPickAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<double>(type: "float", maxLength: 50, nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    OfflineFeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AssigneeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    GeoLocation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AssignTo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PTPAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_ApplicationUser_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_ApplicationUser_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanAccountJSON",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AccountJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAccountJSON", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanAccountJSON_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PaymentGatewayID = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    MerchantReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankTransactionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    TransactionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RRN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AuthCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardHolderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LoanAccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_LoanAccounts_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_PaymentGateways_PaymentGatewayID",
                        column: x => x.PaymentGatewayID,
                        principalTable: "PaymentGateways",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionBatches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountHolderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CollectionBatchOrgId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AcknowledgedById = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    PayInSlipId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectionBatchWorkflowStateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    BatchType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionBatches_ApplicationOrg_CollectionBatchOrgId",
                        column: x => x.CollectionBatchOrgId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionBatches_ApplicationUser_AcknowledgedById",
                        column: x => x.AcknowledgedById,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionBatches_CollectionBatchWorkflowState_CollectionBatchWorkflowStateId",
                        column: x => x.CollectionBatchWorkflowStateId,
                        principalTable: "CollectionBatchWorkflowState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionBatches_PayInSlips_PayInSlipId",
                        column: x => x.PayInSlipId,
                        principalTable: "PayInSlips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOnCommunicationHistoryDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TreatmentHistoryId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LoanAccountId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReasonForFailure = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DispatchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    WAapiResponse = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    WADeliveredStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WADeliveredResponse = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    SubTreatmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    SMSContentCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSContentCreatedTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SMSContentRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSContentRequestTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SMSResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSResponseTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SMSResponseStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailContentCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailContentCreatedTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailContentRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailContentRequestTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailResponseTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailResponseStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WAContentCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WAContentCreatedTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WAContentRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WAContentRequestTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WAResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WAResponseTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WAResponseStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommunicationTemplateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    PaymentTransactionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    SMS_Aggregator_TransactionID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WA_Aggregator_TransactionID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Recipient_Operator = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Recipient_Circle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOnCommunicationHistoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunicationHistoryDetails_CommunicationTemplate_CommunicationTemplateId",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunicationHistoryDetails_LoanAccounts_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunicationHistoryDetails_PaymentTransactions_PaymentTransactionId",
                        column: x => x.PaymentTransactionId,
                        principalTable: "PaymentTransactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunicationHistoryDetails_SubTreatment_SubTreatmentId",
                        column: x => x.SubTreatmentId,
                        principalTable: "SubTreatment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentOnCommunicationHistoryDetails_TreatmentHistory_TreatmentHistoryId",
                        column: x => x.TreatmentHistoryId,
                        principalTable: "TreatmentHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CustomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CollectionMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AreaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EMailId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PayerImageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChangeRequestImageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhysicalReceiptNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AckingAgentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ReceiptId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectionOrgId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectionBatchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CashId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ChequeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    MailSentCount = table.Column<int>(type: "int", nullable: false),
                    SMSSentCount = table.Column<int>(type: "int", nullable: false),
                    TransactionNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AcknowledgedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectionWorkflowStateId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    CancelledCollectionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CancellationRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OfflineCollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GeoLocation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EncredibleUserId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    yForeClosureAmount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yOverdueAmount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yBounceCharges = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    othercharges = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yPenalInterest = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Settlement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yRelationshipWithCustomer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    yPANNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yUploadSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yBatchUploadID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepositBankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepositBankBranch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPoolAccount = table.Column<bool>(type: "bit", nullable: true),
                    IsDepositAccount = table.Column<bool>(type: "bit", nullable: true),
                    ReceiptType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNewPhonenumber = table.Column<bool>(type: "bit", nullable: true),
                    ErrorMessgae = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amountBreakUp1 = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    amountBreakUp2 = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    amountBreakUp3 = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    amountBreakUp4 = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    amountBreakUp5 = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    amountBreakUp6 = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_ApplicationOrg_CollectionOrgId",
                        column: x => x.CollectionOrgId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_ApplicationUser_AckingAgentId",
                        column: x => x.AckingAgentId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_ApplicationUser_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_Cash_CashId",
                        column: x => x.CashId,
                        principalTable: "Cash",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_Cheques_ChequeId",
                        column: x => x.ChequeId,
                        principalTable: "Cheques",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_CollectionBatches_CollectionBatchId",
                        column: x => x.CollectionBatchId,
                        principalTable: "CollectionBatches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_CollectionWorkflowState_CollectionWorkflowStateId",
                        column: x => x.CollectionWorkflowStateId,
                        principalTable: "CollectionWorkflowState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_Collections_CancelledCollectionId",
                        column: x => x.CancelledCollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collections_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AgencyType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "MainType", "SubType" },
                values: new object[,]
                {
                    { "27d4c2e0ce1a438cb44cd7fb8ed552b9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Collections", "Tele calling" },
                    { "ff379ce22f7b4aca9e74d0dadccb3739", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Collections", "Field Agents" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRights_AccountabilityTypeId",
                table: "AccessRights",
                column: "AccountabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRights_ActionMasterId",
                table: "AccessRights",
                column: "ActionMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRights_MenuMasterId",
                table: "AccessRights",
                column: "MenuMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRights_SubMenuMasterId",
                table: "AccessRights",
                column: "SubMenuMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Accountabilities_AccountabilityTypeId",
                table: "Accountabilities",
                column: "AccountabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accountabilities_CommisionerId_ResponsibleId_AccountabilityTypeId",
                table: "Accountabilities",
                columns: new[] { "CommisionerId", "ResponsibleId", "AccountabilityTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActionMaster_SubMenuMasterID",
                table: "ActionMaster",
                column: "SubMenuMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyIdentification_TFlexId",
                table: "AgencyIdentification",
                column: "TFlexId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyIdentification_TFlexIdentificationDocTypeId",
                table: "AgencyIdentification",
                column: "TFlexIdentificationDocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyIdentification_TFlexIdentificationTypeId",
                table: "AgencyIdentification",
                column: "TFlexIdentificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyIdentificationDoc_TFlexIdentificationId",
                table: "AgencyIdentificationDoc",
                column: "TFlexIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPlaceOfWork_AgencyId",
                table: "AgencyPlaceOfWork",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPlaceOfWork_ManagerId",
                table: "AgencyPlaceOfWork",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyScopeOfWork_AgencyId",
                table: "AgencyScopeOfWork",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserDesignation_AgencyUserId",
                table: "AgencyUserDesignation",
                column: "AgencyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserDesignation_DepartmentId",
                table: "AgencyUserDesignation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserDesignation_DesignationId",
                table: "AgencyUserDesignation",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserIdentification_TFlexId",
                table: "AgencyUserIdentification",
                column: "TFlexId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserIdentification_TFlexIdentificationDocTypeId",
                table: "AgencyUserIdentification",
                column: "TFlexIdentificationDocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserIdentification_TFlexIdentificationTypeId",
                table: "AgencyUserIdentification",
                column: "TFlexIdentificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserIdentificationDoc_TFlexIdentificationId",
                table: "AgencyUserIdentificationDoc",
                column: "TFlexIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserPlaceOfWork_AgencyUserId",
                table: "AgencyUserPlaceOfWork",
                column: "AgencyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserScopeOfWork_AgencyUserId",
                table: "AgencyUserScopeOfWork",
                column: "AgencyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserScopeOfWork_DepartmentId",
                table: "AgencyUserScopeOfWork",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyUserScopeOfWork_DesignationId",
                table: "AgencyUserScopeOfWork",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_AddressId",
                table: "ApplicationOrg",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_AgencyCategoryId",
                table: "ApplicationOrg",
                column: "AgencyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_AgencyTypeId",
                table: "ApplicationOrg",
                column: "AgencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_AgencyWorkflowStateId",
                table: "ApplicationOrg",
                column: "AgencyWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_CityId",
                table: "ApplicationOrg",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_CreditAccountDetailsId",
                table: "ApplicationOrg",
                column: "CreditAccountDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_DepartmentId",
                table: "ApplicationOrg",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_DesignationId",
                table: "ApplicationOrg",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_ParentAgencyId",
                table: "ApplicationOrg",
                column: "ParentAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_RecommendingOfficerId",
                table: "ApplicationOrg",
                column: "RecommendingOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AddressId",
                table: "ApplicationUser",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AgencyId",
                table: "ApplicationUser",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AgencyUserWorkflowStateId",
                table: "ApplicationUser",
                column: "AgencyUserWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ApplicationUserDetailsId",
                table: "ApplicationUser",
                column: "ApplicationUserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_BaseBranchId",
                table: "ApplicationUser",
                column: "BaseBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CompanyId",
                table: "ApplicationUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CompanyUserWorkflowStateId",
                table: "ApplicationUser",
                column: "CompanyUserWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CreditAccountDetailsId",
                table: "ApplicationUser",
                column: "CreditAccountDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_SinglePointReportingManagerId",
                table: "ApplicationUser",
                column: "SinglePointReportingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_UserCurrentLocationDetailsId",
                table: "ApplicationUser",
                column: "UserCurrentLocationDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDetails_AddressId",
                table: "ApplicationUserDetails",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaBaseBranchMappings_AreaId",
                table: "AreaBaseBranchMappings",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaBaseBranchMappings_BaseBranchId",
                table: "AreaBaseBranchMappings",
                column: "BaseBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaPinCodeMappings_AreaId",
                table: "AreaPinCodeMappings",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaPinCodeMappings_PinCodeId",
                table: "AreaPinCodeMappings",
                column: "PinCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_BaseBranchId",
                table: "Areas",
                column: "BaseBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_CityId",
                table: "Areas",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_BankId",
                table: "BankBranch",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_CategoryMasterId",
                table: "CategoryItem",
                column: "CategoryMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_ParentId",
                table: "CategoryItem",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_AcknowledgedById",
                table: "CollectionBatches",
                column: "AcknowledgedById");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_CollectionBatchOrgId",
                table: "CollectionBatches",
                column: "CollectionBatchOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_CollectionBatchWorkflowStateId",
                table: "CollectionBatches",
                column: "CollectionBatchWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_PayInSlipId",
                table: "CollectionBatches",
                column: "PayInSlipId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AccountId",
                table: "Collections",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AckingAgentId",
                table: "Collections",
                column: "AckingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CancelledCollectionId",
                table: "Collections",
                column: "CancelledCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CashId",
                table: "Collections",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_ChequeId",
                table: "Collections",
                column: "ChequeId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectionBatchId",
                table: "Collections",
                column: "CollectionBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectionOrgId",
                table: "Collections",
                column: "CollectionOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectionWorkflowStateId",
                table: "Collections",
                column: "CollectionWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectorId",
                table: "Collections",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_ReceiptId",
                table: "Collections",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_CommunicationTemplateDetailId",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserARMScopeOfWork_CompanyUserId",
                table: "CompanyUserARMScopeOfWork",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserARMScopeOfWork_ReportingAgencyId",
                table: "CompanyUserARMScopeOfWork",
                column: "ReportingAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserARMScopeOfWork_SupervisingManagerId",
                table: "CompanyUserARMScopeOfWork",
                column: "SupervisingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserDesignation_CompanyUserId",
                table: "CompanyUserDesignation",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserDesignation_DepartmentId",
                table: "CompanyUserDesignation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserDesignation_DesignationId",
                table: "CompanyUserDesignation",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserPlaceOfWork_CompanyUserId",
                table: "CompanyUserPlaceOfWork",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserScopeOfWork_CompanyUserId",
                table: "CompanyUserScopeOfWork",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserScopeOfWork_DepartmentId",
                table: "CompanyUserScopeOfWork",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserScopeOfWork_DesignationId",
                table: "CompanyUserScopeOfWork",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserScopeOfWork_SupervisingManagerId",
                table: "CompanyUserScopeOfWork",
                column: "SupervisingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccountDetails_BankBranchId",
                table: "CreditAccountDetails",
                column: "BankBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentTypeId",
                table: "Department",
                column: "DepartmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_DepartmentId",
                table: "Designation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_DesignationTypeId",
                table: "Designation",
                column: "DesignationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DispositionCodeMaster_DispositionGroupMasterId",
                table: "DispositionCodeMaster",
                column: "DispositionGroupMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_DispositionValidationMaster_DispositionCodeMasterId",
                table: "DispositionValidationMaster",
                column: "DispositionCodeMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_AccountId",
                table: "Feedback",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_AssigneeId",
                table: "Feedback",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CollectorId",
                table: "Feedback",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlexDynamicBusinessRuleSequences_FlexBusinessContextId",
                table: "FlexDynamicBusinessRuleSequences",
                column: "FlexBusinessContextId");

            migrationBuilder.CreateIndex(
                name: "IX_GeoMaster_BaseBranchId",
                table: "GeoMaster",
                column: "BaseBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_GeoTagDetails_ApplicationUserId",
                table: "GeoTagDetails",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ApplicationUserId",
                table: "Languages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountJSON_AccountId",
                table: "LoanAccountJSON",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_AgencyId",
                table: "LoanAccounts",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_AllocationOwnerId",
                table: "LoanAccounts",
                column: "AllocationOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_CollectorId",
                table: "LoanAccounts",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_LenderId",
                table: "LoanAccounts",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_TeleCallerId",
                table: "LoanAccounts",
                column: "TeleCallerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_TeleCallingAgencyId",
                table: "LoanAccounts",
                column: "TeleCallingAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_TreatmentId",
                table: "LoanAccounts",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MultilingualEntity_CultureId",
                table: "MultilingualEntity",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_MultilingualEntity_MultilingualEntitySetId",
                table: "MultilingualEntity",
                column: "MultilingualEntitySetId");

            migrationBuilder.CreateIndex(
                name: "IX_PayInSlips_PayInSlipWorkflowStateId",
                table: "PayInSlips",
                column: "PayInSlipWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PayInSlips_PrintedById",
                table: "PayInSlips",
                column: "PrintedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_LoanAccountId",
                table: "PaymentTransactions",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentGatewayID",
                table: "PaymentTransactions",
                column: "PaymentGatewayID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CollectorId",
                table: "Receipts",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ReceiptWorkflowStateId",
                table: "Receipts",
                column: "ReceiptWorkflowStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundRobinTreatment_SubTreatmentId",
                table: "RoundRobinTreatment",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundRobinTreatment_TreatmentId",
                table: "RoundRobinTreatment",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_SegmentAdvanceFilterId",
                table: "Segmentation",
                column: "SegmentAdvanceFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_State_RegionId",
                table: "State",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenuMaster_MenuMasterId",
                table: "SubMenuMaster",
                column: "MenuMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTreatment_TreatmentId",
                table: "SubTreatment",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TFlexIdentificationDocTypes_TFlexIdentificationTypeId",
                table: "TFlexIdentificationDocTypes",
                column: "TFlexIdentificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentAndSegmentMapping_SegmentId",
                table: "TreatmentAndSegmentMapping",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentAndSegmentMapping_TreatmentId",
                table: "TreatmentAndSegmentMapping",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentByRule_DepartmentId",
                table: "TreatmentByRule",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentByRule_DesignationId",
                table: "TreatmentByRule",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentByRule_SubTreatmentId",
                table: "TreatmentByRule",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentByRule_TreatmentId",
                table: "TreatmentByRule",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentDesignation_DepartmentId",
                table: "TreatmentDesignation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentDesignation_DesignationId",
                table: "TreatmentDesignation",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentDesignation_SubTreatmentId",
                table: "TreatmentDesignation",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentDesignation_TreatmentId",
                table: "TreatmentDesignation",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentHistory_TreatmentId",
                table: "TreatmentHistory",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnAccount_DepartmentId",
                table: "TreatmentOnAccount",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnAccount_DesignationId",
                table: "TreatmentOnAccount",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnAccount_SubTreatmentId",
                table: "TreatmentOnAccount",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnAccount_TreatmentId",
                table: "TreatmentOnAccount",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunication_CommunicationTemplateId",
                table: "TreatmentOnCommunication",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunication_SubTreatmentId",
                table: "TreatmentOnCommunication",
                column: "SubTreatmentId",
                unique: true,
                filter: "[SubTreatmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunication_TreatmentId",
                table: "TreatmentOnCommunication",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_CommunicationTemplateId",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_LoanAccountId",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_PaymentTransactionId",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "PaymentTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_SubTreatmentId",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_TreatmentHistoryId",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "TreatmentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnPerformanceBand_SubTreatmentId",
                table: "TreatmentOnPerformanceBand",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnPerformanceBand_TreatmentId",
                table: "TreatmentOnPerformanceBand",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnPOS_DepartmentId",
                table: "TreatmentOnPOS",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnPOS_DesignationId",
                table: "TreatmentOnPOS",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnPOS_SubTreatmentId",
                table: "TreatmentOnPOS",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnPOS_TreatmentId",
                table: "TreatmentOnPOS",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnUpdateTrail_SubTreatmentId",
                table: "TreatmentOnUpdateTrail",
                column: "SubTreatmentId",
                unique: true,
                filter: "[SubTreatmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnUpdateTrail_TreatmentId",
                table: "TreatmentOnUpdateTrail",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentQualifyingStatus_SubTreatmentId",
                table: "TreatmentQualifyingStatus",
                column: "SubTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentQualifyingStatus_TreatmentId",
                table: "TreatmentQualifyingStatus",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttendanceDetail_ApplicationUserId",
                table: "UserAttendanceDetail",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttendanceLog_ApplicationUserId",
                table: "UserAttendanceLog",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCustomerPersona_ApplicationUserId",
                table: "UserCustomerPersona",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPerformanceBand_AgencyUserId",
                table: "UserPerformanceBand",
                column: "AgencyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPerformanceBand_CompanyUserId",
                table: "UserPerformanceBand",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSearchCriteria_UserId",
                table: "UserSearchCriteria",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationCodes_UserVerificationCodeTypeId",
                table: "UserVerificationCodes",
                column: "UserVerificationCodeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyIdentification_ApplicationOrg_TFlexId",
                table: "AgencyIdentification",
                column: "TFlexId",
                principalTable: "ApplicationOrg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyPlaceOfWork_ApplicationOrg_AgencyId",
                table: "AgencyPlaceOfWork",
                column: "AgencyId",
                principalTable: "ApplicationOrg",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyPlaceOfWork_ApplicationUser_ManagerId",
                table: "AgencyPlaceOfWork",
                column: "ManagerId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyScopeOfWork_ApplicationOrg_AgencyId",
                table: "AgencyScopeOfWork",
                column: "AgencyId",
                principalTable: "ApplicationOrg",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyUserDesignation_ApplicationUser_AgencyUserId",
                table: "AgencyUserDesignation",
                column: "AgencyUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyUserIdentification_ApplicationUser_TFlexId",
                table: "AgencyUserIdentification",
                column: "TFlexId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyUserPlaceOfWork_ApplicationUser_AgencyUserId",
                table: "AgencyUserPlaceOfWork",
                column: "AgencyUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyUserScopeOfWork_ApplicationUser_AgencyUserId",
                table: "AgencyUserScopeOfWork",
                column: "AgencyUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationOrg_ApplicationUser_RecommendingOfficerId",
                table: "ApplicationOrg",
                column: "RecommendingOfficerId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ApplicationOrg_AgencyId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ApplicationOrg_BaseBranchId",
                table: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "AccessRights");

            migrationBuilder.DropTable(
                name: "Accountabilities");

            migrationBuilder.DropTable(
                name: "AccountLabels");

            migrationBuilder.DropTable(
                name: "AgencyIdentificationDoc");

            migrationBuilder.DropTable(
                name: "AgencyPlaceOfWork");

            migrationBuilder.DropTable(
                name: "AgencyScopeOfWork");

            migrationBuilder.DropTable(
                name: "AgencyUserDesignation");

            migrationBuilder.DropTable(
                name: "AgencyUserIdentificationDoc");

            migrationBuilder.DropTable(
                name: "AgencyUserPlaceOfWork");

            migrationBuilder.DropTable(
                name: "AgencyUserScopeOfWork");

            migrationBuilder.DropTable(
                name: "AllocationDownload");

            migrationBuilder.DropTable(
                name: "AreaBaseBranchMappings");

            migrationBuilder.DropTable(
                name: "AreaPinCodeMappings");

            migrationBuilder.DropTable(
                name: "BankAccountType");

            migrationBuilder.DropTable(
                name: "Buckets");

            migrationBuilder.DropTable(
                name: "BulkTrailUploadFile");

            migrationBuilder.DropTable(
                name: "BulkUploadFile");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "CompanyUserARMScopeOfWork");

            migrationBuilder.DropTable(
                name: "CompanyUserDesignation");

            migrationBuilder.DropTable(
                name: "CompanyUserPlaceOfWork");

            migrationBuilder.DropTable(
                name: "CompanyUserScopeOfWork");

            migrationBuilder.DropTable(
                name: "DepositBankMaster");

            migrationBuilder.DropTable(
                name: "DeviceDetail");

            migrationBuilder.DropTable(
                name: "DispositionValidationMaster");

            migrationBuilder.DropTable(
                name: "FeatureMaster");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FlexDynamicBusinessRuleSequences");

            migrationBuilder.DropTable(
                name: "GeoMaster");

            migrationBuilder.DropTable(
                name: "GeoTagDetails");

            migrationBuilder.DropTable(
                name: "IdConfigMaster");

            migrationBuilder.DropTable(
                name: "IdConfigMaster_SeedData");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LoanAccountJSON");

            migrationBuilder.DropTable(
                name: "MasterFileStatus");

            migrationBuilder.DropTable(
                name: "MultilingualEntity");

            migrationBuilder.DropTable(
                name: "PrimaryAllocationFile");

            migrationBuilder.DropTable(
                name: "PrimaryUnAllocationFile");

            migrationBuilder.DropTable(
                name: "RoundRobinTreatment");

            migrationBuilder.DropTable(
                name: "RunStatus");

            migrationBuilder.DropTable(
                name: "SecondaryAllocationFile");

            migrationBuilder.DropTable(
                name: "SecondaryUnAllocationFile");

            migrationBuilder.DropTable(
                name: "SegmentationAdvanceFilterMasters");

            migrationBuilder.DropTable(
                name: "Sequence");

            migrationBuilder.DropTable(
                name: "TreatmentAndSegmentMapping");

            migrationBuilder.DropTable(
                name: "TreatmentByRule");

            migrationBuilder.DropTable(
                name: "TreatmentDesignation");

            migrationBuilder.DropTable(
                name: "TreatmentHistoryDetails");

            migrationBuilder.DropTable(
                name: "TreatmentOnAccount");

            migrationBuilder.DropTable(
                name: "TreatmentOnCommunication");

            migrationBuilder.DropTable(
                name: "TreatmentOnCommunicationHistoryDetails");

            migrationBuilder.DropTable(
                name: "TreatmentOnPerformanceBand");

            migrationBuilder.DropTable(
                name: "TreatmentOnPOS");

            migrationBuilder.DropTable(
                name: "TreatmentOnUpdateTrail");

            migrationBuilder.DropTable(
                name: "TreatmentQualifyingStatus");

            migrationBuilder.DropTable(
                name: "TreatmentUpdateIntermediate");

            migrationBuilder.DropTable(
                name: "UserAttendanceDetail");

            migrationBuilder.DropTable(
                name: "UserAttendanceLog");

            migrationBuilder.DropTable(
                name: "UserCustomerPersona");

            migrationBuilder.DropTable(
                name: "UserLoginKeys");

            migrationBuilder.DropTable(
                name: "UserPerformanceBand");

            migrationBuilder.DropTable(
                name: "UserPerformanceBandMaster");

            migrationBuilder.DropTable(
                name: "UserPersonaMaster");

            migrationBuilder.DropTable(
                name: "UserSearchCriteria");

            migrationBuilder.DropTable(
                name: "UsersUpdateFile");

            migrationBuilder.DropTable(
                name: "UserVerificationCodes");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "ActionMaster");

            migrationBuilder.DropTable(
                name: "AccountabilityTypes");

            migrationBuilder.DropTable(
                name: "AgencyIdentification");

            migrationBuilder.DropTable(
                name: "AgencyUserIdentification");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "PinCodes");

            migrationBuilder.DropTable(
                name: "Cash");

            migrationBuilder.DropTable(
                name: "Cheques");

            migrationBuilder.DropTable(
                name: "CollectionBatches");

            migrationBuilder.DropTable(
                name: "CollectionWorkflowState");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "DispositionCodeMaster");

            migrationBuilder.DropTable(
                name: "FlexBusinessContext");

            migrationBuilder.DropTable(
                name: "Culture");

            migrationBuilder.DropTable(
                name: "MultilingualEntitySet");

            migrationBuilder.DropTable(
                name: "Segmentation");

            migrationBuilder.DropTable(
                name: "CommunicationTemplate");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "TreatmentHistory");

            migrationBuilder.DropTable(
                name: "SubTreatment");

            migrationBuilder.DropTable(
                name: "UserVerificationCodeTypes");

            migrationBuilder.DropTable(
                name: "SubMenuMaster");

            migrationBuilder.DropTable(
                name: "TFlexIdentificationDocTypes");

            migrationBuilder.DropTable(
                name: "CollectionBatchWorkflowState");

            migrationBuilder.DropTable(
                name: "PayInSlips");

            migrationBuilder.DropTable(
                name: "ReceiptWorkflowState");

            migrationBuilder.DropTable(
                name: "DispositionGroupMaster");

            migrationBuilder.DropTable(
                name: "SegmentationAdvanceFilter");

            migrationBuilder.DropTable(
                name: "CommunicationTemplateDetail");

            migrationBuilder.DropTable(
                name: "CommunicationTemplateWorkflowState");

            migrationBuilder.DropTable(
                name: "LoanAccounts");

            migrationBuilder.DropTable(
                name: "PaymentGateways");

            migrationBuilder.DropTable(
                name: "MenuMaster");

            migrationBuilder.DropTable(
                name: "TFlexIdentificationTypes");

            migrationBuilder.DropTable(
                name: "PayInSlipWorkflowState");

            migrationBuilder.DropTable(
                name: "CategoryItem");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "CategoryMaster");

            migrationBuilder.DropTable(
                name: "ApplicationOrg");

            migrationBuilder.DropTable(
                name: "AgencyCategory");

            migrationBuilder.DropTable(
                name: "AgencyType");

            migrationBuilder.DropTable(
                name: "AgencyWorkflowState");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "AgencyUserWorkflowState");

            migrationBuilder.DropTable(
                name: "ApplicationUserDetails");

            migrationBuilder.DropTable(
                name: "CompanyUserWorkflowState");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CreditAccountDetails");

            migrationBuilder.DropTable(
                name: "UserCurrentLocationDetails");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "DesignationType");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "BankBranch");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "DepartmentType");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
