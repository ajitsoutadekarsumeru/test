namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the file configuration settings.
    /// </summary>
    public class FileConfigurationSettings
    {
        /// <summary>
        /// Gets or sets the delimiter used in the file.
        /// Default is an empty string.
        /// </summary>
        public string Delimiter { get; set; } = "";
        /// <summary>
        /// Gets or sets the default sheet name.
        /// Default is an empty string.
        /// </summary>
        public string DefaultSheet { get; set; } = "Sheet1";

        /// <summary>
        /// Gets or sets the default file extension.
        /// Default is an empty string.
        /// </summary>
        public string DefaultExtension { get; set; } = ".csv";
    
    }
}
