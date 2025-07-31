namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings related to SSIS package configurations, including paths, connections, and templates.
    /// </summary>
    public class SSISPackageSettings
    {
        /// <summary>
        /// Gets or sets the paths settings for SSIS packages, including executable and package locations.
        /// </summary>
        public PathsSettings Paths { get; set; } = new PathsSettings();

        /// <summary>
        /// Gets or sets the connection settings for SSIS packages, including connection strings and provider names.
        /// </summary>
        public ConnectionSettings Connection { get; set; } = new ConnectionSettings();

        /// <summary>
        /// Gets or sets the package settings, including SSIS package project, file extension, and package names.
        /// </summary>
        public PackageSettings PackageSettings { get; set; } = new PackageSettings();

        /// <summary>
        /// Gets or sets the template files settings for SSIS packages, including locations of template files.
        /// </summary>
        public TemplatesFilesSettings TemplatesFiles { get; set; } = new TemplatesFilesSettings();
    }
}