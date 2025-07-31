using ENTiger.ENCollect.Utilities.SSISPackage;

namespace ENTiger.ENCollect
{
    public interface ISSISPackageProvider
    {
        public Task<SSISResultDto> ExecutePackage(BulkUploadFileDto dto, string bulkUploadId);
    }
}