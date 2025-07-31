using CliWrap;
using CliWrap.Buffered;
using ENTiger.ENCollect.Utilities.SSISPackage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect
{
    public class CollectionBulkImportPackage : SSISPackageBase
    {
        protected readonly ILogger<CollectionBulkImportPackage> _logger;
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        public CollectionBulkImportPackage()
        {
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _logger = FlexContainer.ServiceProvider.GetService<ILogger<CollectionBulkImportPackage>>();
        }
        protected override void SetParameters(BulkUploadFileDto dto)
        {
            _logger.LogInformation("Setting parameters for Primary allocation");
            CustomId = dto.CustomId;
            FileName = dto.FileName;
            var tenantId = dto.GetAppContext().TenantId;

            ConnectionPath = _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId)
                              .Select(x => x.DefaultWriteDbConnectionString).FirstOrDefault();
            ConnectionPath = ConnectionPath + "Provider=" + AppConfigManager.GetSection("AppSettings")["ProviderName"];
            DestinationExcelFileName = "BulkTrailStatus_" + CustomId + ".xlsx";
            PackageLocation = AppConfigManager.GetSection("AppSettings")["PackageLocation"];
            ExcelFilePathSourceService = AppConfigManager.GetSection("AppSettings")["ExcelFilePath"];
            //ConnectionPath = AppConfigManager.GetSection("AppSettings")["ConnectionPath"];
            SSISLogPath = AppConfigManager.GetSection("AppSettings")["SSISLogPath"];
            DestinationtExecPath = AppConfigManager.GetSection("AppSettings")["dtExecPath"];
            PackageConnectionString = AppConfigManager.GetSection("AppSettings")["packageConnectionString"];
            SSISPackageFolder = AppConfigManager.GetSection("AppSettings")["SSISPackageFolder"];
            SSISPackageProject = AppConfigManager.GetSection("AppSettings")["SSISPackageProject"];

            ExcelDestinationService = AppConfigManager.GetSection("AppSettings")["ExcelDestinationBulkTrail"];
            ExcelTemplateSourceService = AppConfigManager.GetSection("AppSettings")["ExcelTemplateBulkTrail"];
            PackageName = AppConfigManager.GetSection("AppSettings")["BulkTrailPackageName"];
        }
        protected override async Task<SSISResultDto> Execute(BulkUploadFileDto dto, string bulkUploadId)
        {
            //===============Nservice Team sugest this method=============================
            SSISResultDto sSISResultDto = new SSISResultDto();
            string ExcelSource = ExcelFilePathSourceService + FileName;
            string PkgLocation = PackageLocation + PackageName;// + ".dtsx";
            _logger.LogInformation("PkgLocation: " + PkgLocation);

            _logger.LogInformation("WorkRequestId: " + CustomId);
            _logger.LogInformation("ExcelDestination: " + ExcelDestinationService);
            _logger.LogInformation("ExcelSource: " + ExcelSource);
            _logger.LogInformation("ParamDatabaseConnection: " + ConnectionPath);
            _logger.LogInformation("SSISLogPath: " + SSISLogPath);
            _logger.LogInformation("ParamExcelTemplateSource: " + ExcelTemplateSourceService);

            string arguments = $"/f \"{PkgLocation}\" " +
       $"/SET \"\\Package.Variables[$Package::WorkRequestId]\";\"{CustomId}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamExcelDestination]\";\"{ExcelDestinationService}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamExcelSource]\";\"{ExcelSource}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamExcelTemplateSource]\";\"{ExcelTemplateSourceService}\" " +
        $"/SET \"\\Package.Variables[$Package::ParamDatabaseConnection]\";\"{ConnectionPath}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamSSISLog]\";\"{SSISLogPath}\"";
            _logger.LogInformation("dtExecPath: " + DestinationtExecPath);
            _logger.LogInformation("arguments: " + arguments);

            try
            {
                var stdOutBuffer = new StringBuilder();
                var stdErrBuffer = new StringBuilder();
                _logger.LogInformation("start to DTExe process");

                var result = await Cli.Wrap(DestinationtExecPath)
                    .WithArguments([arguments])
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();
                _logger.LogInformation("End to DTExe process");

                // Output the results
                System.Diagnostics.Trace.WriteLine($"Standard Output: {result.StandardOutput}");
                System.Diagnostics.Trace.WriteLine($"Standard Error: {result.StandardError}");
                _logger.LogInformation($"Standard Output: {result.StandardOutput}");
                _logger.LogInformation($"Standard Error: {result.StandardError}");

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
                _logger.LogInformation("End to waiting output exit");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred: {ex.Message}");
                sSISResultDto.PackageExecResult = "Failure";
                throw;
            }
            return sSISResultDto;
        }
    }
}