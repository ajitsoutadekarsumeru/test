namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the configuration settings for Microsoft-related services.
    /// </summary>
    public class MicrosoftSettings
    {
        /// <summary>
        /// Gets or sets the provider string for Excel operations.
        /// Typically used for connecting to Excel files.
        /// </summary>
        /// <example>
        /// Example value: "Microsoft.ACE.OLEDB.16.0"
        /// </example>
        public string ExcelProvider { get; set; } = "Microsoft.ACE.OLEDB.16.0";
    }
}