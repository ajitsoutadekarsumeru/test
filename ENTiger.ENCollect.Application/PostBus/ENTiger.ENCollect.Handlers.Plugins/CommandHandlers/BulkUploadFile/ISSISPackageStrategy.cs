using ENTiger.ENCollect.FeedbackModule;

public interface ISSISPackageStrategy
{
    void SetParameters(BulkTrailUploadCommand command, string TenantId);

    Task<SSISResult> ExecutePackage(BulkTrailUploadCommand command, string bulkuploadid);
}