public class FileValidationSettings
{
    public string DefaultFolder { get; set; } = null; // Set this.
    public int MaximumFileSizeMb { get; set; } = 2;
    public int MaxDownloadFileSizeMb { get; set; } = 2;

    public int MaxInsightReportDownloadFileSizeInMb { get; set; } = 10;
    public int MaxNumberOfFiles { get; set; } = 1;
}