using ENTiger.ENCollect.DomainModels.BulkUploadFile;

public class SSISResultProcessor
{
    public void ProcessResult(SSISResult result, BulkUploadFile Buf, string TenantId)
    {
        if (result.PackageExecResult != "Success")
        {
            Buf.UpdateStatus("Processing Failed");
        }
        else
        {
            Buf.ProcessResult(result.RowsProcessed, result.RowsSuccess, result.RowsError);
            Buf.SetDestinationFile(result.DestinationFilePath, result.DestinationFileName);
        }

        //var BulkUploadRepos = new BulkUploadFileRepo();
        //BulkUploadRepos.SaveBulkUploadFile(Buf, TenantId);
    }
}