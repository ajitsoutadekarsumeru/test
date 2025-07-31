namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for Active Directory configuration.
    /// </summary>
    public class ActiveDirectorySettings
    {
        /// <summary>
        /// Gets or sets the directory-related settings.
        /// </summary>
        public DirectorySettings Directory { get; set; } = new DirectorySettings();

        /// <summary>
        /// Gets or sets the validation-related settings.
        /// </summary>
        public DomainValidation DomainValidation { get; set; } = new DomainValidation();

        /// <summary>
        /// Gets or sets the default password for validation purposes.
        /// </summary>
        /// <example>
        /// Example value: "123@asAS"
        /// </example>
        public string DefaultPassword { get; set; } = "";
    }
}