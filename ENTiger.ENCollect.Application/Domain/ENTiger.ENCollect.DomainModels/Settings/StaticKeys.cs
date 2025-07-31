namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the static encryption and decryption keys used for secure data processing.
    /// </summary>
    public class StaticKeys
    {
        /// <summary>
        /// Gets or sets the decryption key used for decrypting data.
        /// </summary>
        public string DecryptionKey { get; set; } = "";

        /// <summary>
        /// Gets or sets the encryption key used for encrypting data.
        /// </summary>
        public string EncryptionKey { get; set; } = "";
    }
}