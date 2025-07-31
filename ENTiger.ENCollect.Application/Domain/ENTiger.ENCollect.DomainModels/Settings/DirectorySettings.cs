namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for the Active Directory path and domain.
    /// </summary>
    public class DirectorySettings
    {
        /// <summary>
        /// Gets or sets the LDAP path for the Active Directory.
        /// </summary>
        /// <example>
        /// Example value: "LDAP://bankofbaroda.co.in"
        /// </example>
        public string Path { get; set; } = "";

        /// <summary>
        /// Gets or sets the domain name for the Active Directory.
        /// </summary>
        /// <example>
        /// Example value: "BANKOFBARODA"
        /// </example>
        public string Domain { get; set; } = "";
    }
}