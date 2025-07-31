using ENTiger.ENCollect.FeedbackModule;

public class SSISExecutor
{
    private readonly ISSISPackageStrategy _strategy;

    public SSISExecutor(ISSISPackageStrategy strategy)
    {
        _strategy = strategy;
    }

    public async Task<SSISResult> PrepareAndExecute(BulkTrailUploadCommand command, string bulkuploadid, string TenantId)
    {
        _strategy.SetParameters(command, TenantId);
        return await _strategy.ExecutePackage(command, bulkuploadid);
    }
}