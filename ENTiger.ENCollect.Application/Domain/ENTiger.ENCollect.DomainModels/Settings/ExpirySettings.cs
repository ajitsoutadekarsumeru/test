namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the expiry settings for password reset and OTP functionality.
    /// </summary>
    public class ExpirySettings
    {
        /// <summary>
        /// Gets or sets the expiry time (in minutes) for the forgot password token.
        /// Default is 10 minutes.
        /// </summary>
        public int ForgotPasswordOtpInMins { get; set; } = 10;

        /// <summary>
        /// Gets or sets the expiry time (in minutes) for the OTP (One-Time Password).
        /// Default is 10 minutes.
        /// </summary>
        public int LoginOtpInMins { get; set; } = 10;

        /// <summary>
        /// Gets or sets the Payment expiry time (in minutes) for the OTP (One-Time Password).
        /// Default is 10 minutes.
        /// </summary>
        public int PaymentPluginOtpInMins { get; set; } = 10;
    }
}