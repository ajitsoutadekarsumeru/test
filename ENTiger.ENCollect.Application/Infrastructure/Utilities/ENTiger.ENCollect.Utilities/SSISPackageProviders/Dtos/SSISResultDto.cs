namespace ENTiger.ENCollect.Utilities.SSISPackage
{
    public class SSISResultDto : DtoBridge
    {
        public string? PackageExecResult { get; set; } //Success or Failure
        public int? RowsProcessed { get; set; }
        public int? RowsSuccess { get; set; }
        public int? RowsError { get; set; }
        public string? DestinationFilePath { get; set; }
        public string? DestinationFileName { get; set; }
    }
}