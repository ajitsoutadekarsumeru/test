namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for various messages, including login-related messages.
    /// </summary>
    public class AuthorizationCardSettings
    {
        /// <summary>
        /// Days before card expiry to notify.
        /// </summary>
        public int ExpiryNotificationInDays { get; set; }
    }
}
