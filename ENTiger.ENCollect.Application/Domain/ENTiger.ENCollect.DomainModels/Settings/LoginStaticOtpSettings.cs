namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for static OTP (One-Time Password) functionality.
    /// </summary>
    public class StaticOtpSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether static OTP functionality is enabled.
        /// Default is false.
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether the OTP bypass flag is enabled.
        /// Default is true.
        /// </summary>
        public string Otp { get; set; } = "";
    }
}