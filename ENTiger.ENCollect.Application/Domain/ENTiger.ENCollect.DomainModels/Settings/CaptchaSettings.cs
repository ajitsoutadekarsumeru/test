namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings required for CAPTCHA verification.
    /// </summary>
    public class CaptchaSettings
    {
        /// <summary>
        /// Gets or sets the URL used for CAPTCHA verification.
        /// </summary>
        public string Url { get; set; } = "";

        /// <summary>
        /// Gets or sets the secret key used for CAPTCHA validation.
        /// </summary>
        public string SecretKey { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether Captcha functionality is enabled.
        /// Default is false.
        /// </summary>
        public bool Enabled { get; set; } = false;
    }
}