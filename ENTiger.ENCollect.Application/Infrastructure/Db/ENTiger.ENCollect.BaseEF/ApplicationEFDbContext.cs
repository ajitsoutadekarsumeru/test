using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect;

public class ApplicationEFDbContext : FlexEFDbContext
{
    public ApplicationEFDbContext()
    {
        //this constructor goes only with the migrations
    }

    public ApplicationEFDbContext(DbContextOptions options) : base(options)
    {
        //this constructor is being used by the repos to initialize the context with various options including multitenancy

        //Uncomment below code to enable audit trail for your CUD transactions
        //this.EnableAuditTrail();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //
        //modelBuilder.Entity<CompanyUser>().HasMany<CompanyUserARMScopeOfWork>(a => a.ARMScopeOfWork).WithOne(p => p.CompanyUser);
        //modelBuilder.Entity<CompanyUser>().HasMany<CompanyUserScopeOfWork>(a => a.ScopeOfWork).WithOne(p => p.CompanyUser);

        //Use this for adding indexes or other customizations to tables pertaining to EF implementation
        modelBuilder.Entity<AgencyType>().HasData(AgencyType.GetSeedData());

        modelBuilder.Entity<Wallet>(wallet =>
        {
            // Use AgentId as the key.
            wallet.HasKey(w => w.AgentId);

            // Configure Reservations as an owned collection.
            wallet.OwnsMany(w => w.Reservations, r =>
            {
                r.WithOwner().HasForeignKey("WalletAgentId");
                r.HasKey(r => r.Id);
            });
        });

        modelBuilder.Entity<Settlement>(b =>
        {
            b.HasKey(s => s.Id);

            b.HasOne(s => s.LatestHistory)
             .WithMany()      // no backref needed
             .HasForeignKey(s => s.LatestHistoryId)
             .OnDelete(DeleteBehavior.Restrict);
        });

            // Settlement -> Projection (1:N)
            modelBuilder.Entity<Settlement>()
                .HasMany(s => s.QueueProjections)
                .WithOne(p => p.Settlement)
                .HasForeignKey(p => p.SettlementId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ApplicationUser>()
                       .HasOne(u => u.Wallet)
                        .WithOne(w => w.Agent)
                        .HasForeignKey<Wallet>(w => w.AgentId)
                        .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> Projection (1:N)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AssignedQueueProjections)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> Projection (1:N)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserLevelProjections)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Collection>()
                        .HasIndex(c => c.CollectionMode)
                        .HasDatabaseName("IX_Collections_CollectionMode");
            modelBuilder.Entity<Collection>()
                        .HasIndex(c => c.CustomerName)
                        .HasDatabaseName("IX_Collections_CustomerName");
            modelBuilder.Entity<Collection>()
                        .HasIndex(c => c.CreatedDate)
                        .HasDatabaseName("IX_Collections_CreatedDate");
            modelBuilder.Entity<Collection>()
                        .HasIndex(c => c.AcknowledgedDate)
                        .HasDatabaseName("IX_Collections_AcknowledgedDate");
            modelBuilder.Entity<Collection>()
                        .HasIndex(c => c.CollectionDate)
                        .HasDatabaseName("IX_Collections_CollectionDate");

        modelBuilder.Entity<Cheque>()
                    .HasIndex(c => c.InstrumentDate)
                    .HasDatabaseName("IX_Cheques_InstrumentDate");

        modelBuilder.Entity<CommunicationTemplate>()
                    .HasIndex(c => c.CreatedDate)
                    .HasDatabaseName("IX_CommunicationTemplate_CreatedDate");
        modelBuilder.Entity<CommunicationTemplate>()
                    .HasIndex(c => c.TemplateType)
                    .HasDatabaseName("IX_CommunicationTemplate_TemplateType");
        modelBuilder.Entity<CommunicationTemplate>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_CommunicationTemplate_Name");

        modelBuilder.Entity<ApplicationOrg>()
                    .HasIndex(c => c.FirstName)
                    .HasDatabaseName("IX_ApplicationOrg_FirstName");
        modelBuilder.Entity<ApplicationOrg>()
                    .HasIndex(c => c.LastName)
                    .HasDatabaseName("IX_ApplicationOrg_LastName");

        modelBuilder.Entity<AgencyUser>()
                    .HasIndex(c => c.AuthorizationCardExpiryDate)
                    .HasDatabaseName("IX_ApplicationUser_AuthorizationCardExpiryDate");

        modelBuilder.Entity<Agency>()
                    .HasIndex(c => c.ContractExpireDate)
                    .HasDatabaseName("IX_ApplicationOrg_ContractExpireDate");

        modelBuilder.Entity<ApplicationUser>()
                    .HasIndex(c => c.PrimaryEMail)
                    .HasDatabaseName("IX_ApplicationUser_PrimaryEMail");

        modelBuilder.Entity<AgencyWorkflowState>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_AgencyWorkflowState_Name");

        modelBuilder.Entity<Accountability>()
                    .HasIndex(c => c.AccountabilityTypeId)
                    .HasDatabaseName("IX_Accountabilities_AccountabilityTypeId");

        modelBuilder.Entity<CompanyUserWorkflowState>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_CompanyUserWorkflowState_Name");

        modelBuilder.Entity<CompanyUser>()
                    .HasIndex(c => c.FirstName)
                    .HasDatabaseName("IX_CompanyUser_FirstName");
        modelBuilder.Entity<CompanyUser>()
                    .HasIndex(c => c.LastName)
                    .HasDatabaseName("IX_CompanyUser_LastName");

        modelBuilder.Entity<Area>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Area_Name");
        modelBuilder.Entity<Area>()
                    .HasIndex(c => c.NickName)
                    .HasDatabaseName("IX_Area_NickName");

        modelBuilder.Entity<BulkTrailUploadFile>()
                    .HasIndex(c => c.FileUploadedDate)
                    .HasDatabaseName("IX_BulkTrailUploadFile_FileUploadedDate");

        modelBuilder.Entity<CategoryItem>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_CategoryItem_Name");
        modelBuilder.Entity<CategoryItem>()
                    .HasIndex(c => c.Code)
                    .HasDatabaseName("IX_CategoryItem_Code");
        modelBuilder.Entity<CategoryItem>()
                    .HasIndex(c => c.CategoryMasterId)
                    .HasDatabaseName("IX_CategoryItem_CategoryMasterId");

        modelBuilder.Entity<CategoryMaster>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_CategoryMaster_Name");

        modelBuilder.Entity<Cities>()
                    .HasIndex(c => c.NickName)
                    .HasDatabaseName("IX_Cities_NickName");

        modelBuilder.Entity<CollectionBatch>()
                    .HasIndex(c => c.ProductGroup)
                    .HasDatabaseName("IX_CollectionBatches_ProductGroup");
        modelBuilder.Entity<CollectionBatch>()
                    .HasIndex(c => c.BatchType)
                    .HasDatabaseName("IX_CollectionBatches_BatchType");
        modelBuilder.Entity<CollectionBatch>()
                    .HasIndex(c => c.ModeOfPayment)
                    .HasDatabaseName("IX_CollectionBatches_ModeOfPayment");
        modelBuilder.Entity<CollectionBatch>()
                    .HasIndex(c => c.CreatedDate)
                    .HasDatabaseName("IX_CollectionBatches_CreatedDate");

        modelBuilder.Entity<Countries>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Countries_Name");
        modelBuilder.Entity<Countries>()
                    .HasIndex(c => c.NickName)
                    .HasDatabaseName("IX_Countries_NickName");

        modelBuilder.Entity<Department>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Department_Name");
        modelBuilder.Entity<Department>()
                    .HasIndex(c => c.Code)
                    .HasDatabaseName("IX_Department_Code");

        modelBuilder.Entity<Designation>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Designation_Name");
        modelBuilder.Entity<Designation>()
                    .HasIndex(c => c.Acronym)
                    .HasDatabaseName("IX_Designation_Acronym");

        modelBuilder.Entity<FeatureMaster>()
                    .HasIndex(c => c.Parameter)
                    .HasDatabaseName("IX_FeatureMaster_Parameter");

        modelBuilder.Entity<Feedback>()
                    .HasIndex(c => c.DispositionCode)
                    .HasDatabaseName("IX_Feedback_DispositionCode");

        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.ProductCode)
                    .HasDatabaseName("IX_LoanAccounts_ProductCode");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.BranchCode)
                    .HasDatabaseName("IX_LoanAccounts_BranchCode");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.CITY)
                    .HasDatabaseName("IX_LoanAccounts_City");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.STATE)
                    .HasDatabaseName("IX_LoanAccounts_State");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.BUCKET)
                    .HasDatabaseName("IX_LoanAccounts_Bucket");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.BILLING_CYCLE)
                    .HasDatabaseName("IX_LoanAccounts_BillingCycle");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.PAYMENTSTATUS)
                    .HasDatabaseName("IX_LoanAccounts_PaymentStatus");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.ProductGroup)
                    .HasDatabaseName("IX_LoanAccounts_ProductGroup");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.SubProduct)
                    .HasDatabaseName("IX_LoanAccounts_SubProduct");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.NPA_STAGEID)
                    .HasDatabaseName("IX_LoanAccounts_NPAStageId");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.DispCode)
                    .HasDatabaseName("IX_LoanAccounts_DispCode");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.Region)
                    .HasDatabaseName("IX_LoanAccounts_Region");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.LatestPTPDate)
                    .HasDatabaseName("IX_LoanAccounts_LatestPTPDate");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.DateOfBirth)
                    .HasDatabaseName("IX_LoanAccounts_DateOfBirth");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.LastUploadedDate)
                    .HasDatabaseName("IX_LoanAccounts_LastUploadedDate");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.LatestFeedbackDate)
                    .HasDatabaseName("IX_LoanAccounts_LatestFeedbackDate");
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.LatestPaymentDate)
                    .HasDatabaseName("IX_LoanAccounts_LatestPaymentDate");
        modelBuilder.Entity<LoanAccount>()
                    .Property(c => c.ReverseOfAgreementId)
                    .HasComputedColumnSql("REVERSE(AgreementId)", stored: true);
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.ReverseOfAgreementId)
                    .HasDatabaseName("IX_LoanAccounts_ReverseOfAgreementId");
        modelBuilder.Entity<LoanAccount>()
                    .Property(c => c.ReverseOfPrimaryCard)
                    .HasComputedColumnSql("REVERSE(PRIMARY_CARD_NUMBER)", stored: true);
        modelBuilder.Entity<LoanAccount>()
                    .HasIndex(c => c.ReverseOfPrimaryCard)
                    .HasDatabaseName("IX_LoanAccounts_ReverseOfPrimaryCard");

        modelBuilder.Entity<MasterFileStatus>()
                    .HasIndex(c => c.FileName)
                    .HasDatabaseName("IX_MasterFileStatus_FileName");
        modelBuilder.Entity<MasterFileStatus>()
                    .HasIndex(c => c.Status)
                    .HasDatabaseName("IX_MasterFileStatus_Status");
        modelBuilder.Entity<MasterFileStatus>()
                    .HasIndex(c => c.UploadType)
                    .HasDatabaseName("IX_MasterFileStatus_UploadType");
        modelBuilder.Entity<MasterFileStatus>()
                    .HasIndex(c => c.FileUploadedDate)
                    .HasDatabaseName("IX_MasterFileStatus_FileUploadedDate");

        modelBuilder.Entity<PayInSlip>()
                    .HasIndex(c => c.ModeOfPayment)
                    .HasDatabaseName("IX_PayInSlips_ModeOfPayment");
        modelBuilder.Entity<PayInSlip>()
                    .HasIndex(c => c.ProductGroup)
                    .HasDatabaseName("IX_PayInSlips_ProductGroup");
        modelBuilder.Entity<PayInSlip>()
                    .HasIndex(c => c.PayinslipType)
                    .HasDatabaseName("IX_PayInSlips_PayinslipType");
        modelBuilder.Entity<PayInSlip>()
                    .HasIndex(c => c.CreatedDate)
                    .HasDatabaseName("IX_PayInSlips_CreatedDate");

        modelBuilder.Entity<PrimaryAllocationFile>()
                    .HasIndex(c => c.FileUploadedDate)
                    .HasDatabaseName("IX_PrimaryAllocationFile_FileUploadedDate");
        modelBuilder.Entity<PrimaryUnAllocationFile>()
                    .HasIndex(c => c.UploadedDate)
                    .HasDatabaseName("IX_PrimaryUnAllocationFile_UploadedDate");

        modelBuilder.Entity<Regions>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Regions_Name");
        modelBuilder.Entity<Regions>()
                    .HasIndex(c => c.NickName)
                    .HasDatabaseName("IX_Regions_NickName");

        modelBuilder.Entity<SecondaryAllocationFile>()
                    .HasIndex(c => c.FileName)
                    .HasDatabaseName("IX_SecondaryAllocationFile_FileName");
        modelBuilder.Entity<SecondaryAllocationFile>()
                    .HasIndex(c => c.FileUploadedDate)
                    .HasDatabaseName("IX_SecondaryAllocationFile_FileUploadedDate");

        modelBuilder.Entity<SecondaryUnAllocationFile>()
                    .HasIndex(c => c.UploadedDate)
                    .HasDatabaseName("IX_SecondaryUnAllocationFile_UploadedDate");

        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.ProductGroup)
                    .HasDatabaseName("IX_Segmentation_ProductGroup");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.Product)
                    .HasDatabaseName("IX_Segmentation_Product");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.SubProduct)
                    .HasDatabaseName("IX_Segmentation_SubProduct");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.BOM_Bucket)
                    .HasDatabaseName("IX_Segmentation_BOM_Bucket");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.CurrentBucket)
                    .HasDatabaseName("IX_Segmentation_CurrentBucket");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.Zone)
                    .HasDatabaseName("IX_Segmentation_Zone");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.State)
                    .HasDatabaseName("IX_Segmentation_State");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.City)
                    .HasDatabaseName("IX_Segmentation_City");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.ExecutionType)
                    .HasDatabaseName("IX_Segmentation_ExecutionType");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Segmentation_Name");
        modelBuilder.Entity<Segmentation>()
                    .HasIndex(c => c.CreatedDate)
                    .HasDatabaseName("IX_Segmentation_CreatedDate");

        modelBuilder.Entity<State>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_State_Name");
        modelBuilder.Entity<State>()
                    .HasIndex(c => c.NickName)
                    .HasDatabaseName("IX_State_NickName");

        modelBuilder.Entity<Treatment>()
                    .HasIndex(c => c.Name)
                    .HasDatabaseName("IX_Treatment_Name");
        modelBuilder.Entity<Treatment>()
                    .HasIndex(c => c.Mode)
                    .HasDatabaseName("IX_Treatment_Mode");

        modelBuilder.Entity<TreatmentOnCommunicationHistoryDetails>()
                    .HasIndex(c => c.DeliveryDate)
                    .HasDatabaseName("IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate");
        modelBuilder.Entity<TreatmentOnCommunicationHistoryDetails>()
                    .Property(c => c.DeliveryDate_Only)
                    .HasComputedColumnSql("CAST(DeliveryDate AS DATE)", stored: true);
        modelBuilder.Entity<TreatmentOnCommunicationHistoryDetails>()
                    .HasIndex(c => c.DeliveryDate_Only)
                    .HasDatabaseName("IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate_Only");

        modelBuilder.Entity<UsersCreateFile>()
                    .HasIndex(c => c.Status)
                    .HasDatabaseName("IX_UsersCreateFile_Status");

        modelBuilder.Entity<UserVerificationCodeTypes>()
                    .HasIndex(c => c.Description)
                    .HasDatabaseName("IX_UserVerificationCodeTypes_Description");

        modelBuilder.Entity<UserAttendanceLog>()
                    .HasIndex(c => c.CreatedDate)
                    .HasDatabaseName("IX_UserAttendanceLog_CreatedDate");

        modelBuilder.Entity<UserAttendanceDetail>()
                    .HasIndex(c => c.Date)
                    .HasDatabaseName("IX_UserAttendanceDetail_Date");

        modelBuilder.Entity<UsersUpdateFile>()
                    .HasIndex(c => c.UploadedDate)
                    .HasDatabaseName("IX_UsersUpdateFile_UploadedDate");
        modelBuilder.Entity<AccountScopeConfiguration>().HasData(AccountScopeConfiguration.GetSeedData());

        modelBuilder.Entity<LoanAccountsProjection>()
                    .HasIndex(e => new { e.LoanAccountId, e.Year, e.Month })
                    .IsUnique();

        modelBuilder.Entity<LoanAccountsProjection>()
                    .Property(p => p.Version)
                    .IsConcurrencyToken();

        modelBuilder.Entity<CommunicationTemplate>()
                    .Property(p => p.Version)
                    .IsConcurrencyToken();

        modelBuilder.Entity<CommunicationTemplateDetail>()
                    .Property(p => p.Version)
                    .IsConcurrencyToken();

        modelBuilder.Entity<Resolution>()
                    .Property(e => e.Code)
                    .HasConversion<string>().HasMaxLength(50);


        modelBuilder.Entity<HeatMapConfig>()
                    .Property(e => e.HeatIndicator)
                    .HasConversion<string>().HasMaxLength(50);

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ILogger _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<ApplicationEFDbContext>>();

        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .EnableSensitiveDataLogging()
            .LogTo(message => _logger.LogDebug(message)) //This is to log the EF queries to the logger
                                                         //.AddInterceptors(new DefaultCreationTimeInterceptor()); //This is to set the creation time for the entities
            .AddInterceptors(new DateTimeNowWithoutOffsetInterceptor());
    }
}