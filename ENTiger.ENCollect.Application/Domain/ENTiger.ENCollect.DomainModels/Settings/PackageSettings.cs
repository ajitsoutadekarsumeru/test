namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings related to SSIS package configurations and file extensions.
    /// </summary>
    public class PackageSettings
    {
        /// <summary>
        /// Gets or sets the folder path where SSIS packages are stored.
        /// </summary>
        public string SSISPackageFolder { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the SSIS package project.
        /// </summary>
        public string SSISPackageProject { get; set; } = "";

        /// <summary>
        /// Gets or sets the file extension used for SSIS packages.
        /// </summary>
        public string FileExtension { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the bulk trail SSIS package.
        /// </summary>
        public string BulkTrailPackageName { get; set; } = "";
    }
}