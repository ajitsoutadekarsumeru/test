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
    public class SecondaryAllocationPackage : SSISPackageBase
    {
        protected readonly ILogger<SecondaryAllocationPackage> _logger;
        private readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        private readonly SSISPackageSettings _ssisPackageSettings;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;

        public SecondaryAllocationPackage()
        {
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _logger = FlexContainer.ServiceProvider.GetService<ILogger<SecondaryAllocationPackage>>();
            var ssisOptions = FlexContainer.ServiceProvider.GetService<IOptions<SSISPackageSettings>>();
            _ssisPackageSettings = ssisOptions?.Value ?? new SSISPackageSettings();
            var fileOptions = FlexContainer.ServiceProvider.GetService<IOptions<FilePathSettings>>();
            _fileSettings = fileOptions?.Value ?? new FilePathSettings();           // Assuming default constructor is available
            _fileSystem = new FileSystem();
        }

        public SecondaryAllocationPackage(
            ILogger<SecondaryAllocationPackage> logger,
            IFlexTenantRepository<FlexTenantBridge> repoTenantFactory,
            IOptions<SSISPackageSettings> ssisPackageSettings,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem)
        {
            _logger = logger;
            _repoTenantFactory = repoTenantFactory;
            _ssisPackageSettings = ssisPackageSettings.Value;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        protected override void SetParameters(BulkUploadFileDto dto)
        {
            _logger.LogInformation("Setting parameters for Secondary allocation");

            AllocationType = dto.FileType;
            CustomId = dto.CustomId;
            FileName = dto.FileName;
            var tenantId = dto.GetAppContext().TenantId;

            ConnectionPath = _repoTenantFactory
                .FindAll<FlexTenantBridge>()
                .Where(x => x.Id == tenantId)
                .Select(x => x.DefaultWriteDbConnectionString)
                .FirstOrDefault();


            ConnectionPath += $"Provider={_ssisPackageSettings.Connection.ProviderName}";
            DestinationExcelFileName = $"{AllocationType}_AllocationStatus_{CustomId}.xlsx";
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
                _fileSettings.AllocationProcessedFilePath)
                + Path.DirectorySeparatorChar;

            // Construct `configPrefix`
            string configPrefix = $"SecondaryAllocation_{AllocationType?.Trim().Replace(" ", "")}_{dto.AllocationMethod?.Trim().Replace(" ", "")}";

            // Dynamically generate `templateFileName`
            string templateFileName = GetTemplateFileName(configPrefix);
            ExcelTemplateSourceService = _fileSystem.Path.Combine(
                _fileSettings.BasePath,
                _fileSettings.IncomingPath,
                _fileSettings.AllocationProcessedFilePath,
                templateFileName
            );

            PackageName = $"{configPrefix}.dtsx";

            _logger.LogInformation("End Setting parameters for Secondary allocation");
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

        protected override async Task<SSISResultDto> Execute(BulkUploadFileDto dto, string bulkUploadId)
        {
            SSISResultDto sSISResultDto = new SSISResultDto();
            string ExcelSource = _fileSystem.Path.Combine(ExcelFilePathSourceService, FileName);
            string PkgLocation = _fileSystem.Path.Combine(PackageLocation, PackageName);

            _logger.LogInformation($"Executing SSIS Package: {PkgLocation}");
            _logger.LogInformation($"AllocationType: {AllocationType}, WorkRequestId: {CustomId}");
            _logger.LogInformation($"ExcelDestination: {ExcelDestinationService}, ExcelSource: {ExcelSource}");
            _logger.LogInformation($"DatabaseConnection: {ConnectionPath}, SSISLogPath: {SSISLogPath}");
            _logger.LogInformation($"ExcelTemplateSource: {ExcelTemplateSourceService}");


            string arguments = string.Format(
             "/f \"{0}\" " +
             "/SET \"\\Package.Variables[$Package::ParamAllocationType]\";\"{1}\" " +
             "/SET \"\\Package.Variables[$Package::WorkRequestId]\";\"{2}\" " +
             "/SET \"\\Package.Variables[$Package::ParamExcelDestination]\";\"{3}\" " +
             "/SET \"\\Package.Variables[$Package::ParamExcelSource]\";\"{4}\" " +
             "/SET \"\\Package.Variables[$Package::ParamExcelTemplateSource]\";\"{5}\" " +
             "/SET \"\\Package.Variables[$Package::ParamDatabaseConnection]\";\"{6}\" " +
             "/SET \"\\Package.Variables[$Package::ParamSSISLog]\";\"{7}\"",
             PkgLocation, AllocationType.ToLower(), CustomId, ExcelDestinationService, ExcelSource, ExcelTemplateSourceService, ConnectionPath, SSISLogPath);

            _logger.LogInformation($"dtExecPath: {DestinationtExecPath}, arguments: {arguments}");

            try
            {
                _logger.LogInformation("Starting SSIS Execution Process...");

                var result = await Cli.Wrap(DestinationtExecPath)
                    .WithArguments([arguments])
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();

                _logger.LogInformation("SSIS Execution Process Completed.");

                _logger.LogInformation($"Standard Output: {result.StandardOutput}");
                _logger.LogInformation($"Standard Error: {result.StandardError}");

                if (!string.IsNullOrEmpty(result.StandardError) || result.StandardOutput.Contains("DTSER_FAILURE"))
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
                _logger.LogError($"An error occurred: {ex.Message}");
                sSISResultDto.PackageExecResult = "Failure";
                throw;
            }

            return sSISResultDto;
        }
    }
}
