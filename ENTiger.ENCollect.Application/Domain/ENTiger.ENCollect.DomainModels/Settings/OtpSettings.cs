namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings related to OTP (One-Time Password) functionality.
    /// </summary>
    public class OtpSettings
    {
        /// <summary>
        /// Gets or sets the static OTP settings, including configurations for OTP generation and validation.
        /// </summary>
        public StaticOtpSettings StaticOtp { get; set; } = new StaticOtpSettings();

        /// <summary>
        /// Gets or sets the expiry settings for OTP and related tokens.
        /// </summary>
        public ExpirySettings Expiry { get; set; } = new ExpirySettings();
    }
}