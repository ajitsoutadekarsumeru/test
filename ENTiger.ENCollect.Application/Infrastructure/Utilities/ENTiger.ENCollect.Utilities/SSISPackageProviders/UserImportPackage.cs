using ENTiger.ENCollect.Utilities.SSISPackage;

namespace ENTiger.ENCollect
{
    public class UserImportPackage : ISSISPackageProvider
    {
        public async Task<SSISResultDto> ExecutePackage(BulkUploadFileDto dto, string bulkUploadId)
        {
            SetParameters(dto);
            return null;
        }

        private void SetParameters(BulkUploadFileDto dto)
        {
        }
    }
}