using CliWrap;
using CliWrap.Buffered;
using ENTiger.ENCollect.Utilities.SSISPackage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.Reflection;
using System.Text;

namespace ENTiger.ENCollect
{
    public class BulkTrailImportPackage : SSISPackageBase
    {
        protected readonly ILogger<BulkTrailImportPackage> _logger;
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        private readonly SSISPackageSettings _ssisPackageSettings;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        public BulkTrailImportPackage()
        {
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _logger = FlexContainer.ServiceProvider.GetService<ILogger<BulkTrailImportPackage>>();
            var ssisOptions = FlexContainer.ServiceProvider.GetService<IOptions<SSISPackageSettings>>();
            _ssisPackageSettings = ssisOptions?.Value ?? new SSISPackageSettings();
            var fileOptions = FlexContainer.ServiceProvider.GetService<IOptions<FilePathSettings>>();
            _fileSettings = fileOptions?.Value ?? new FilePathSettings();           // Assuming default constructor is available
            _fileSystem = new FileSystem();                    // Assuming default constructor is available
        }
        public BulkTrailImportPackage(
          ILogger<BulkTrailImportPackage> logger,
          IFlexTenantRepository<FlexTenantBridge> repoTenantFactory,
          IOptions<SSISPackageSettings> ssisPackageSettings,
          IOptions<FilePathSettings> fileSettings,
          IFileSystem fileSystem)
        {
            _logger = logger;
            _repoTenantFactory = repoTenantFactory;
            _ssisPackageSettings = ssisPackageSettings?.Value;
            _fileSettings = fileSettings?.Value;
            _fileSystem = fileSystem;
        }
        protected override void SetParameters(BulkUploadFileDto dto)
        {
            _logger.LogInformation("Setting parameters for Primary allocation");
            CustomId = dto.CustomId;
            FileName = dto.FileName;
            var tenantId = dto.GetAppContext().TenantId;

            ConnectionPath = _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId)
                              .Select(x => x.DefaultWriteDbConnectionString).FirstOrDefault();
            ConnectionPath += $"Provider={_ssisPackageSettings.Connection.ProviderName}";
            DestinationExcelFileName = "BulkTrailStatus_" + CustomId + ".xlsx";
            PackageLocation = _ssisPackageSettings.Paths.PackageLocation;

            ExcelFilePathSourceService = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            SSISLogPath = _ssisPackageSettings.Paths.PackageLogPath;
            DestinationtExecPath = _ssisPackageSettings.Paths.ExecutablePath;
            PackageConnectionString = _ssisPackageSettings.Connection.ConnectionString;
            SSISPackageFolder = _ssisPackageSettings.PackageSettings.SSISPackageFolder;
            SSISPackageProject = _ssisPackageSettings.PackageSettings.SSISPackageProject;

            ExcelDestinationService = _fileSystem.Path.Combine(
              _fileSettings.BasePath,
              _fileSettings.IncomingPath,
              _fileSettings.BulkTrailProcessedFilePath)
              + Path.DirectorySeparatorChar;
            // Construct `configPrefix`
            string configPrefix = $"ExcelTemplateBulkTrail";

            // Dynamically generate `templateFileName`
            string templateFileName = GetTemplateFileName(configPrefix);
            ExcelTemplateSourceService = _fileSystem.Path.Combine(
                _fileSettings.BasePath,
                _fileSettings.IncomingPath,
                _fileSettings.BulkTrailProcessedFilePath,
                templateFileName
            );
            PackageName = $"BulkTrailUploadFile.dtsx";
        }
        protected override async Task<SSISResultDto> Execute(BulkUploadFileDto dto, string bulkUploadId)
        {
            //===============Nservice Team sugest this method=============================
            SSISResultDto sSISResultDto = new SSISResultDto();
            string ExcelSource = _fileSystem.Path.Combine(ExcelFilePathSourceService, FileName);
            string PkgLocation = _fileSystem.Path.Combine(PackageLocation, PackageName);

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

        private string GetTemplateFileName(string configPrefix)
        {
            // Log the exact value of configPrefix for debugging
            _logger.LogInformation($"Template Name for Primary allocation configPrefix:{configPrefix}");

            // Normalize configPrefix: trim and convert to lowercase
            var normalizedConfigPrefix = configPrefix?.Trim().ToLower();  // Ensure configPrefix is not null

            // Log the normalized configPrefix for further debugging
            _logger.LogInformation($"Normalized configPrefix: '{normalizedConfigPrefix}'");

            var templatesFilesSettings = _ssisPackageSettings.TemplatesFiles;

            // Get all the properties of TemplatesFilesSettings and convert to a dictionary with lowercase keys
            var templatesDict = typeof(TemplatesFilesSettings)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(
                    prop => prop.Name.ToLower(),  // Normalize property names to lowercase
                    prop => prop.GetValue(templatesFilesSettings)?.ToString() ?? string.Empty  // Get values and handle null
                );

            // Log the available template keys for debugging
            _logger.LogInformation("Available template keys: " + string.Join(", ", templatesDict.Keys));

            // Log full comparison details to catch potential issues with spaces or hidden characters
            foreach (var templateKey in templatesDict.Keys)
            {
                _logger.LogInformation($"Checking against template key: '{templateKey}'");
            }

            // Find the template by normalized configPrefix (case insensitive match)
            var matchedTemplate = templatesDict
                .FirstOrDefault(kvp => normalizedConfigPrefix == kvp.Key);  // Check if the keys match exactly

            // Log whether a match was found or not
            if (matchedTemplate.Equals(default(KeyValuePair<string, string>)))
            {
                // Log the error if no template was found
                _logger.LogError($"No matching template found for '{normalizedConfigPrefix}'. Available templates: {string.Join(", ", templatesDict.Keys)}");
                throw new KeyNotFoundException($"No template found for {normalizedConfigPrefix}");
            }

            // Log the matched template for debugging
            _logger.LogInformation($"Matched template: '{matchedTemplate.Value}'");

            return matchedTemplate.Value;
        }
    }
}