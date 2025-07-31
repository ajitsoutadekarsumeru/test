public class SSISResult
{
    public string PackageExecResult; //Success or Failure
    public int RowsProcessed;
    public int RowsSuccess;
    public int RowsError;
    public string DestinationFilePath;
    public string DestinationFileName;
}