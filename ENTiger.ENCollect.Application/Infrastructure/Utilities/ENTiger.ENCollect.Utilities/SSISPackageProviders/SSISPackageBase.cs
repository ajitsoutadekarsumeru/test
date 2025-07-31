using ENTiger.ENCollect.Utilities.SSISPackage;

namespace ENTiger.ENCollect
{
    public abstract class SSISPackageBase : ISSISPackageProvider
    {
        #region Attributes

        public string? DestinationExcelFileName { get; set; } = string.Empty;
        public string? ExcelDestinationService { get; set; } = string.Empty;
        public string? ExcelTemplateSourceService { get; set; } = string.Empty;
        public string? PackageName { get; set; } = string.Empty;
        public string? PackageLocation { get; set; } = string.Empty;
        public string? ExcelFilePathSourceService { get; set; } = string.Empty;
        public string? ConnectionPath { get; set; } = string.Empty;
        public string? SSISLogPath { get; set; } = string.Empty;
        public string? AllocationType { get; set; } = string.Empty;
        public string? CustomId { get; set; } = string.Empty;
        public string? FileName { get; set; } = string.Empty;
        public string? DestinationtExecPath { get; set; } = string.Empty;
        public string? PackageConnectionString { get; set; } = string.Empty;
        public string? SSISPackageFolder { get; set; } = string.Empty;
        public string? SSISPackageProject { get; set; } = string.Empty;

        #endregion Attributes

        public async Task<SSISResultDto> ExecutePackage(BulkUploadFileDto dto, string bulkUploadId)
        {
            SetParameters(dto);
            SSISResultDto sSISResultDto = await Execute(dto, bulkUploadId);
            return sSISResultDto;
        }

        protected abstract void SetParameters(BulkUploadFileDto dto);

        protected abstract Task<SSISResultDto> Execute(BulkUploadFileDto dto, string bulkUploadId);
    }
}