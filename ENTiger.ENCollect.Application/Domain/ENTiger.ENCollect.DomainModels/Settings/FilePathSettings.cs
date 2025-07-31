namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for file and folder paths used in the application.
    /// </summary>
    public class FilePathSettings
    {
        public string BasePath { get; set; } = "";
        /// <summary>
        /// Gets or sets the path to the upload folder.
        /// </summary>
        public string IncomingPath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the trail gap report folder.
        /// </summary>
        public string TrailGapReportPath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the temporary folder.
        /// </summary>
        public string TemporaryPath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the unallocation folder.
        /// </summary>
        public string UnAllocationProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the unallocation folder.
        /// </summary>
        public string AllocationProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the user file folder.
        /// </summary>
        public string UserProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the destination folder.
        /// </summary
        public string BulkTrailProcessedFilePath { get; set; } = "";
        /// <summary>
        /// Folder for storing and processing bulk collection-specific files, including processed or failed files during bulk upload creation.
        /// Folder path will be e.g., BasePath + IncomingPath + BulkCollectionProcessedFilePath (e.g., "F:/uploads/bulkcollection").
        /// </summary>
        public string BulkCollectionProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Folder for storing successfully processed files, typically after validation and processing steps.
        /// Folder path will be e.g., BasePath + IncomingPath + SuccessProcessedFilePath (e.g., "F:/uploads/success").
        /// </summary>
        public string SuccessProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Folder for storing failed files that did not pass validation or processing rules.
        /// Folder path will be e.g., BasePath + IncomingPath + FailedProcessedFilePath (e.g., "F:/uploads/failed").
        /// </summary>
        public string FailedProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Folder for storing partially processed files where some records succeeded while others failed.
        /// Folder path will be e.g., BasePath + IncomingPath + PartialProcessedFilePath (e.g., "F:/uploads/partial").
        /// </summary>
        public string PartialProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Folder for storing invalid files that failed due to format issues or critical data errors.
        /// Folder path will be e.g., BasePath + IncomingPath + InvalidProcessedFilePath (e.g., "F:/uploads/invalid").
        /// </summary>
        public string InvalidProcessedFilePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the insight reports folder.
        /// </summary>
        public string InsightReportFilePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the path to the Backup folder.
        /// </summary>
        public string BackupFilePath { get; set; } = "";
        /// <summary>
        /// Gets or sets the path to the Template folder.
        /// </summary>
        public string TemplateFilePath { get; set; } = "";
    }
}