using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ENCollect.ApiManagement.RateLimiter;
using ENCollect.Security;
using ENTiger.Core.Utilities;
using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.DomainModels.Settings;
using ENTiger.ENCollect.DomainModels.Utilities;
using ENTiger.ENCollect.DomainModels.Utilities.ELK_Utilities;
using ENTiger.ENCollect.DomainModels.Utilities.File_Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Configuration;
using System.IO.Abstractions;
using ENCollect.ApiManagement.RateLimiter;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using ENCollect.Security;
using ENCollect.Dyna.Workflows;
using ENCollect.Dyna.Filters;
using System.Security.Cryptography;


namespace ENTiger.ENCollect
{
    public static class OtherApplicationServicesConfig
    {
        public static void AddOtherBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add LicenseService to DI container
            services.AddSingleton<ILicenseService, LicenseService>();

            // load the file into its own IConfiguration
            var licenseConfig = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("license.json", optional: false, reloadOnChange: false)
                                    .Build();
            // bind the entire file to LicenseInfo
            services.Configure<LicenseInfo>(licenseConfig);

            // Add this to re-validate whenever reloadOnChange: true is set in ConfigureCommonHost for license.json
            //services.AddOptions<LicenseInfo>()
            //        .Bind(configuration.GetSection("License"))
            //        .Validate(license =>
            //        {
            //            // Recompute the HMAC and compare
            //            var computed = LicenseUtility.GenerateSignature(license, Convert.FromBase64String(configuration["License:SecretKey"]));
            //            return computed == license.Signature
            //                   && license.ExpiresOn > DateTime.UtcNow;
            //        }, "Invalid or expired license")
            //        .ValidateOnStart();  // optional: fail-fast on startup

            services.AddTransient<IRepoFactory, RepoFactory>();
            services.AddTransient<IAuditTrailManager, InDbAuditTrailManager>();
            services.AddTransient<ITreatmentCommonFunctions, TreatmentCommonFunctions>();
            services.AddTransient<IDiffGenerator, DiffGenerator>();
            services.AddTransient<IWalletRepository, WalletRepository>();

            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IPermissionSchemeRepository, PermissionSchemeRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IGeoTagRepository, GeoTagRepository>();
            services.AddTransient<IPayInSlipRepository, PayInSlipRepository>();
            services.AddTransient<ICollectionRepository, CollectionRepository>();

            services.AddTransient<IParameterContext, ParameterContext>();
            services.AddTransient(typeof(IWorkflowNavigator<>), typeof(StandardWorkflowNavigator<>));
            services.AddTransient(typeof(IStepActorResolver<>), typeof(CascadingFlowActorResolver<>));

            services.AddTransient<ISettlementRepository, SettlementRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ITriggerTypeRepository, TriggerTypeRepository>();
            services.AddTransient<ICommunicationTemplateRepository, CommunicationTemplateRepository>();
            services.AddTransient<ITemplateParser, TemplateParser>();
            services.AddTransient<ITemplateProcessor, TemplateProcessor>();
            services.AddTransient<ITemplateProcessor, TemplateProcessor>();
            services.AddTransient<IRecipientDataService, RecipientDataService>();
            services.AddTransient<ICommunicationTriggerRepository, CommunicationTriggerRepository>();
            services.AddTransient<ILoanAccountProjectionService, LoanAccountProjectionService>();
            services.AddScoped<DynamicLoanAccountFieldFetcherService>();

            services.AddTransient<IAccountFetchStrategy, OnAgencyAllocationChangeAccountFetchStrategy>();
            services.AddTransient<IAccountFetchStrategy, OnBrokenPtpAccountFetchStrategy>();
            services.AddTransient<IAccountFetchStrategy, OnPTPDateAccountFetchStrategy>();
            services.AddTransient<IAccountFetchStrategy, XDaysAfterStatementDateFetchStrategy>();
            services.AddTransient<IAccountFetchStrategy, XDaysBeforeDueDateAccountFetchStrategy>();
            services.AddTransient<IAccountFetchStrategy, XDaysPastDueAccountFetchStrategy>();


            services.AddSingleton<DynaWorkflowDefinition<SettlementContext>>(
           _ => SettlementWorkflowDefinitionFactory.Create()
            );
          

            services.AddSingleton<DbUtilityFactory>();
            services.AddTransient<IDbUtility, MySqlUtility>();
            services.AddTransient<MySqlUtility>();
            services.AddTransient<MsSqlUtility>();
            services.AddTransient<AesGcmCrypto>();

            // Register HttpClient for dependencies
            services.AddHttpClient();

            // Register dependencies properly
            services.AddTransient<IClientPostingStrategy, ClientCBSPostingStrategy>();
            services.AddTransient<ClientCBSPostingStrategy>();  // Register concrete class explicitly
            services.AddTransient<ClientPostingStrategyFactory>();

            services.AddTransient<ICollectionPoster, CBSCollectionPoster>();
            services.AddTransient<ICollectionBatchPoster, CBSCollectionBatchPoster>();
            services.AddTransient<IPayInSlipPoster, CBSPayInSlipPoster>();
            services.AddTransient<AesGcmCrypto>();

            services.AddTransient<ICustomUtility, CustomUtility>();
            services.AddTransient<IELKUtility, ElasticUtility>();
            services.AddTransient<IUserUtility, UserUtility>();
            services.AddTransient<IDataTableUtility, DataTableUtility>();
            services.AddTransient<IRoleSearchScopeUtility, RoleSearchScopeUtility>();
            services.AddScoped<CheckUserLoginActivity>();
            services.AddTransient<CheckUserSessionAccess>();
            services.AddTransient<RepoFactory>();  // Transient lifetime for RepoFactory
            services.AddTransient<IPermissionService, PermissionService>();  // Transient lifetime for PermissionService
            services.AddScoped<ITenantConnectionFactory, TenantConnectionFactory>();
            services.AddScoped<IMySqlBulkLoaderService, MySqlBulkLoaderAdapter>();
            services.AddScoped<IRoleSearchScopeUtility, RoleSearchScopeUtility>();
            services.AddScoped<IMySqlCommandService, MySqlCommandAdapter>();
            services.AddScoped<ISqlBulkCopyService, SqlBulkCopyAdapter>();
            services.AddScoped<ISqlCommandService, SqlCommandAdapter>();

            services.AddScoped<ILoanAccountQueryRepository, LoanAccountQueryRepository>();
            services.AddScoped<ILoanAccountContactHistoryQueryRepository, LoanAccountContactHistoryQueryRepository>();


            services.AddScoped<IAccountScopeConfigurationQueryRepository, AccountScopeConfigurationQueryRepository>();
            services.AddScoped<IAccountabilityQueryRepository, AccountabilityQueryRepository>();
            services.AddScoped<IApplicationUserQueryRepository, ApplicationUserQueryRepository>();

            services.AddScoped<ICommunicationTriggerRepository, CommunicationTriggerRepository>();

            services.AddScoped<AccountScopeEvaluatorService>();
            


            services.AddHttpClient<DistanceCalculatorService>();
            services.AddSingleton<IDistanceCalculatorService>(sp =>
                sp.GetRequiredService<DistanceCalculatorService>());
           

            // --- DI & Polly Configuration for the API Client ---
            services.AddHttpClient<IApiHelper, ApiHelper>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            #region FileUtilities

            services.AddSingleton<IFileSystem, FileSystem>(); // from Abstractions

            services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));

            services.AddTransient<IFileValidationUtility, FileValidationUtility>();

            services.Configure<FileValidationSettings>(configuration.GetSection("FileValidationSettings"));

            services.AddTransient<IFileTransferUtility, FileTransferUtility>();

            services.Configure<FileTransferSettings>(configuration.GetSection("FileTransferSettings"));

            services.AddTransient<ICsvExcelUtility, CsvExcelUtility>();

            services.Configure<CsvExcelSettings>(configuration.GetSection("CsvExcelSettings"));

            services.Configure<DatabaseSettings>(configuration.GetSection("FlexBase"));

            services.Configure<FilePathSettings>(configuration.GetSection("FilePathSettings"));

            services.Configure<ElasticSearchSettings>(configuration.GetSection("ElasticSearchSettings"));

            services.Configure<NotificationSettings>(configuration.GetSection("NotificationSettings"));

            services.Configure<SessionSettings>(configuration.GetSection("SessionSettings"));

            services.Configure<EncryptionSettings>(configuration.GetSection("EncryptionSettings"));

            services.Configure<OtpSettings>(configuration.GetSection("OtpSettings"));

            services.Configure<FileSizeSettings>(configuration.GetSection("FileSizeSettings"));

            services.Configure<ConcurrencySettings>(configuration.GetSection("ConcurrencySettings"));

            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));

            services.Configure<GoogleSettings>(configuration.GetSection("GoogleSettings"));

            services.Configure<AuthorizationCardSettings>(configuration.GetSection("AuthorizationCardSettings"));

            services.Configure<MobileSettings>(configuration.GetSection("MobileSettings"));

            services.Configure<ServiceControlSettings>(configuration.GetSection("ServiceControlSettings"));

            services.Configure<FileConfigurationSettings>(configuration.GetSection("FileConfigurationSettings"));

            services.Configure<LocationSettings>(configuration.GetSection("LocationSettings"));

            services.Configure<RestClientSettings>(configuration.GetSection("RestClientSettings"));

            services.Configure<UserFieldSettings>(configuration.GetSection("UserFieldSettings"));

            services.Configure<PasswordSettings>(configuration.GetSection("PasswordSettings"));

            services.Configure<AccountImportSettings>(configuration.GetSection("AccountImportSettings"));

            services.Configure<PaymentSettings>(configuration.GetSection("PaymentSettings"));

            services.Configure<CronJobSettings>(configuration.GetSection("CronJobSettings"));

            services.Configure<SystemUserSettings>(configuration.GetSection("SystemUserSettings"));
            services.Configure<WalletSettings>(configuration.GetSection("WalletSettings"));

            services.Configure<CannedReportSetting>(configuration.GetSection("CannedReportSetting"));
            services.Configure<FrontendUrlSettings>(configuration.GetSection("FrontendUrlSettings"));

            services.Configure<CronJobSettings>(configuration.GetSection("CronJobSettings"));

            services.Configure<AccountExpiryColorSettings>(configuration.GetSection("AccountExpiryColorSettings"));

            services.Configure<SystemUserSettings>(configuration.GetSection("SystemUserSettings"));
            services.Configure<LoginSettings>(configuration.GetSection("LoginSettings"));
            services.Configure<LicenseSettings>(configuration.GetSection("LicenseSettings"));

            services.Configure<LicenseColorSettings>(configuration.GetSection("LicenseColorSettings"));

            #endregion FileTransferUtility

            #region MySql related

            # endregion MySql related

            services.AddMemoryCache();

            // Register the rate limiter from ENCollect.ApiManagement.RateLimiter
            services.AddSingleton<IRateLimiter, RateLimiter>();


            services.AddTransient<RepoFactory>();

            services.Configure<ElasticSearchSettings>(configuration.GetSection("ElasticSearchSettings"));

            
            // Configures Elasticsearch client and registers it as a singleton in the dependency injection container.
            // Elasticsearchclient can be injected wherever its needed and DI container will automatically provide configured instance.
            services.AddSingleton<ElasticsearchClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<ElasticSearchSettings>>().Value;
                
                var esSettings = new ElasticsearchClientSettings(new Uri(settings.Connection.Url))
                    .Authentication(new BasicAuthentication(settings.Connection.Username, settings.Connection.Password));
                return new ElasticsearchClient(esSettings);

            });


            services.AddTransient<IElasticSearchService, ElasticSearchService>();

            services.Configure<ElasticSearchIndexSettings>(configuration.GetSection("ElasticSearchIndexSettings"));



        }

        #region Polly Policies Configuration

        /// <summary>
        /// Defines a retry policy that retries 3 times with exponential backoff for transient HTTP errors.
        /// </summary>
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        /// <summary>
        /// Defines a circuit breaker policy that breaks the circuit after 5 consecutive errors for 30 seconds.
        /// </summary>
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        #endregion
    }
}
