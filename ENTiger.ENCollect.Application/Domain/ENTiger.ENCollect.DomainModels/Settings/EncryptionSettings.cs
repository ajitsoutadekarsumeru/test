namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings required for encryption configurations.
    /// </summary>
    public class EncryptionSettings
    {
        /// <summary>
        /// Gets or sets the static keys used for encryption and decryption.
        /// </summary>
        public StaticKeys StaticKeys { get; set; } = new StaticKeys();
    }
}