namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the file size settings for upload and download operations.
    /// </summary>
    public class FileSizeSettings
    {
        /// <summary>
        /// Gets or sets the maximum file size allowed for uploads.
        /// </summary>
        public int UploadFileSizeInMb { get; set; } = 0;

        /// <summary>
        /// Gets or sets the maximum file size allowed for downloads.
        /// </summary>
        public int DownloadFileSizeInMb { get; set; } = 0;
    }
}