using CliWrap;
using CliWrap.Buffered;
using ENTiger.ENCollect.Utilities.SSISPackage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect
{
    public class AccountImportPackage : SSISPackageBase
    {
        protected readonly ILogger<AccountImportPackage> _logger;
        private readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;

        public AccountImportPackage()
        {
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _logger = FlexContainer.ServiceProvider.GetService<ILogger<AccountImportPackage>>();
        }

        protected override void SetParameters(BulkUploadFileDto dto)
        {
            _logger.LogInformation("Setting parameters for Account Import");

            CustomId = dto.CustomId;
            FileName = dto.FileName;
            var tenantId = dto.GetAppContext().TenantId;

            ConnectionPath = _repoTenantFactory.FindAll<FlexTenantBridge>()
                                .Where(x => x.Id == tenantId)
                                .Select(x => x.DefaultWriteDbConnectionString)
                                .FirstOrDefault();

            // Load configuration values
            ConnectionPath = ConnectionPath + "Provider=" + AppConfigManager.GetSection("AppSettings")["ProviderName"];
            PackageLocation = AppConfigManager.GetSection("AppSettings")["PackageLocation"];
            ExcelFilePathSourceService = AppConfigManager.GetSection("AppSettings")["ExcelFilePath"];
            SSISLogPath = AppConfigManager.GetSection("AppSettings")["SSISLogPath"];
            DestinationtExecPath = AppConfigManager.GetSection("AppSettings")["dtExecPath"];
            PackageConnectionString = AppConfigManager.GetSection("AppSettings")["packageConnectionString"];
            SSISPackageFolder = AppConfigManager.GetSection("AppSettings")["SSISPackageFolder"];
            SSISPackageProject = AppConfigManager.GetSection("AppSettings")["SSISPackageProject"];
            ExcelDestinationService = AppConfigManager.GetSection("AppSettings")["ExcelDestinationAccountImport"];
            PackageName = AppConfigManager.GetSection("AppSettings")["AccountImportPackageName"];

            DestinationExcelFileName = $"AccountImportStatus_{CustomId}.xlsx";
        }

        protected override async Task<SSISResultDto> Execute(BulkUploadFileDto dto, string bulkUploadId)
        {
            SSISResultDto sSISResultDto = new SSISResultDto();
            string excelSource = Path.Combine(ExcelFilePathSourceService, FileName);
            string pkgLocation = Path.Combine(PackageLocation, PackageName); // Full path of the SSIS package

            _logger.LogInformation("SSIS Package Details: ");
            _logger.LogInformation("PkgLocation: {PkgLocation}", pkgLocation);
            _logger.LogInformation("WorkRequestId: {CustomId}", CustomId);
            _logger.LogInformation("ExcelDestination: {ExcelDestinationService}", ExcelDestinationService);
            _logger.LogInformation("ExcelSource: {ExcelSource}", excelSource);
            _logger.LogInformation("ParamDatabaseConnection: {ConnectionPath}", ConnectionPath);
            _logger.LogInformation("SSISLogPath: {SSISLogPath}", SSISLogPath);

            // Build the dtExec arguments
            string arguments = $"/f \"{pkgLocation}\" " +
      $"/SET \"\\Package.Variables[$Package::WorkRequestId]\";\"{CustomId}\" " +
      $"/SET \"\\Package.Variables[$Package::ParamExcelDestination]\";\"{ExcelDestinationService}\" " +
      $"/SET \"\\Package.Variables[$Package::ParamExcelSource]\";\"{excelSource}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamDatabaseConnection]\";\"{ConnectionPath}\" " +
      $"/SET \"\\Package.Variables[$Package::ParamSSISLog]\";\"{SSISLogPath}\"";

            _logger.LogInformation("dtExecPath: {dtExecPath}", DestinationtExecPath);
            _logger.LogInformation("Arguments: {Arguments}", arguments);

            try
            {
                var stdOutBuffer = new StringBuilder();
                var stdErrBuffer = new StringBuilder();

                _logger.LogInformation("Starting dtExec process...");

                var result = await Cli.Wrap(DestinationtExecPath)
                    .WithArguments([arguments])
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();
                _logger.LogInformation("End to DTExe process");

                // Log standard output and error
                _logger.LogInformation("Standard Output: {Output}", result.StandardOutput);
                _logger.LogInformation("Standard Error: {Error}", result.StandardError);

                // Check if there was an error
                if (!string.IsNullOrEmpty(result.StandardError))
                {
                    _logger.LogError("SSIS package execution encountered an error.");
                    sSISResultDto.PackageExecResult = "Failure";
                }
                else
                {
                    _logger.LogInformation("SSIS package executed successfully.");
                    sSISResultDto.PackageExecResult = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during SSIS package execution.");
                sSISResultDto.PackageExecResult = "Failure";
                throw;
            }

            return sSISResultDto;
        }
    }
}