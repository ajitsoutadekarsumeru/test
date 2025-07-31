using CliWrap;
using CliWrap.Buffered;
using ENTiger.ENCollect;
using ENTiger.ENCollect.FeedbackModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text;

public class FeedbackTrailStrategy : ISSISPackageStrategy
{
    protected readonly ILogger<FeedbackTrailStrategy> _logger;
    private string DestinationExcelFileName;
    private string ExcelDestinationBulkTrailService;
    private string ExcelTemplateSourceBulkTrailService;
    private string PackageName;
    private string PackageLocation;
    private string ExcelFilePathService;
    private string ConnectionPath;
    private string SSISLogPath;
    public string allocationType;
    public string customId;
    public string fileName;
    public string dtExecPath;
    private string packageConnectionString;
    private string SSISPackageFolder;
    private string SSISPackageProject;

    public void SetParameters(BulkTrailUploadCommand command, string TenantId)
    {
        System.Diagnostics.Trace.WriteLine("Setting parameters for BulkTrail allocation");
        var configuration = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>();
        _logger.LogInformation("Before connectionstring");
        //MySqlConnectionDBParam param = InitFlexEF.MySqlGetConnection(command.TenantId);
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();

        string sqlConnectionString = _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == TenantId).Select(x => x.DefaultWriteDbConnectionString).FirstOrDefault();

        ConnectionPath = sqlConnectionString;
        _logger.LogInformation("after connectionstring : " + ConnectionPath);
        //  allocationType = command.Model.AllocationType;
        customId = command.CustomId;
        fileName = command.Dto.BulkTrailFileName;

        DestinationExcelFileName = "BulkTrailStatus_" + customId + ".xlsx";
        PackageLocation = configuration.GetSection("AppSettings")["PackageLocation"];
        ExcelFilePathService = configuration.GetSection("AppSettings")["ExcelFilePath"];
        //ConnectionPath = configuration.GetSection("AppSettings")["ConnectionPath"];
        SSISLogPath = configuration.GetSection("AppSettings")["SSISLogPath"];
        dtExecPath = configuration.GetSection("AppSettings")["dtExecPath"];
        packageConnectionString = configuration.GetSection("AppSettings")["packageConnectionString"];
        SSISPackageFolder = configuration.GetSection("AppSettings")["SSISPackageFolder"];
        SSISPackageProject = configuration.GetSection("AppSettings")["SSISPackageProject"];

        ExcelDestinationBulkTrailService = configuration.GetSection("AppSettings")["ExcelDestinationBulkTrail"];
        ExcelTemplateSourceBulkTrailService = configuration.GetSection("AppSettings")["ExcelTemplateBulkTrail"];
        PackageName = configuration.GetSection("AppSettings")["BulkTrailPackageName"];
    }

    public async Task<SSISResult> ExecutePackage(BulkTrailUploadCommand command, string bulkuploadid)
    {
        //===============Nservice Team sugest this method=============================
        string ExcelSource = ExcelFilePathService + fileName;
        string PkgLocation = PackageLocation + PackageName;// + ".dtsx";
        _logger.LogInformation("PkgLocation: " + PkgLocation);

        //logger.LogInformation("allocationType: " + allocationType);
        _logger.LogInformation("WorkRequestId: " + customId);
        _logger.LogInformation("ExcelDestination: " + ExcelDestinationBulkTrailService);
        _logger.LogInformation("ExcelSource: " + ExcelSource);
        _logger.LogInformation("ParamDatabaseConnection: " + ConnectionPath);
        _logger.LogInformation("SSISLogPath: " + SSISLogPath);
        _logger.LogInformation("ParamExcelTemplateSource: " + ExcelTemplateSourceBulkTrailService);

        string arguments = $"/f \"{PkgLocation}\" " +
       $"/SET \"\\Package.Variables[$Package::WorkRequestId]\";\"{customId}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamExcelDestination]\";\"{ExcelDestinationBulkTrailService}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamExcelSource]\";\"{ExcelSource}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamExcelTemplateSource]\";\"{ExcelTemplateSourceBulkTrailService}\" " +
        $"/SET \"\\Package.Variables[$Package::ParamDatabaseConnection]\";\"{ConnectionPath}\" " +
       $"/SET \"\\Package.Variables[$Package::ParamSSISLog]\";\"{SSISLogPath}\"";
        _logger.LogInformation("dtExecPath: " + dtExecPath);
        _logger.LogInformation("arguments: " + arguments);
        try
        {
            var stdOutBuffer = new StringBuilder();
            var stdErrBuffer = new StringBuilder();
            _logger.LogInformation("start to DTExe process");

            var result = await Cli.Wrap(dtExecPath)
                .WithArguments([arguments])
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
            _logger.LogInformation("End to DTExe process");

            // Output the results
            System.Diagnostics.Trace.WriteLine($"Standard Output: {result.StandardOutput}");
            System.Diagnostics.Trace.WriteLine($"Standard Error: {result.StandardError}");
            _logger.LogInformation("End to waiting output exit");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"An error occurred: {ex.Message}");
            throw;
        }

        return null;

        //================ SQL Through SSIS package Execution =================================

        //string ExcelSource = ExcelFilePathService + fileName;
        //logger.LogInformation("packageConnectionString: " + packageConnectionString);
        //var executor = new SsisPackageExecutor(packageConnectionString);

        //logger.LogInformation("PackageName: " + PackageName);
        // logger.LogInformation("WorkRequestId: " + customId);
        //logger.LogInformation("ExcelDestination: " + ExcelDestinationBulkTrailService);
        //logger.LogInformation("ExcelSource: " + ExcelSource);
        //logger.LogInformation("ParamDatabaseConnection: " + ConnectionPath);
        //logger.LogInformation("SSISLogPath: " + SSISLogPath);
        //logger.LogInformation("ParamExcelTemplateSource: " + ExcelTemplateSourceBulkTrailService);

        //var parameters = new Dictionary<string, object>
        //{
        //    // Add your SSIS package parameters here
        //     {"WorkRequestId", customId},
        //     {"ParamExcelDestination", ExcelDestinationBulkTrailService},
        //     {"ParamExcelSource", ExcelSource},
        //     {"ParamExcelTemplateSource", ExcelTemplateSourceBulkTrailService},
        //     {"ParamDatabaseConnection", ConnectionPath},
        //     {"ParamSSISLog", SSISLogPath}
        //};
        //try
        //{
        //    //await executor.ExecuteSsisPackageAsync("SSISPackageFolder", "SSISPackageProject", "SecondaryBulkUploadFile.dtsx", parameters);
        //    await executor.ExecuteSsisPackageAsync(SSISPackageFolder, SSISPackageProject, PackageName, parameters);
        //}
        //catch (Exception ex)
        //{
        //    System.Diagnostics.Trace.WriteLine($"An error occurred: {ex.Message}");
        //    throw;
        //}

        //return null;
    }
}

//public class UserUploadStrategy : ISSISPackageStrategy
//{
//    public void SetParameters(BulkUploadFileCommand command)
//    {
//        System.System.Diagnostics.Trace.WriteLine("Setting parameters for user upload");
//        // Use properties of command to set parameters specific to Primary Allocation package
//    }

//    public SSISResult ExecutePackage(BulkUploadFileCommand command, string bulkuploadid)
//    {
//        System.System.Diagnostics.Trace.WriteLine("Executing user upload SSIS package");
//        return new SSISResult();
//    }
//}