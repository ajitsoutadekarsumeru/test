namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings related to file paths used in the application.
    /// </summary>
    public class PathsSettings
    {
        /// <summary>
        /// Gets or sets the path where executable files are located.
        /// </summary>
        public string ExecutablePath { get; set; } = "";

        /// <summary>
        /// Gets or sets the location of the package files.
        /// </summary>
        public string PackageLocation { get; set; } = "";

        /// <summary>
        /// Gets or sets the path where log files are stored.
        /// </summary>
        public string PackageLogPath { get; set; } = "";
    }
}