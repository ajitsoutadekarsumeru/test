namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for notifications, including SMS signature.
    /// </summary>
    public class NotificationSettings
    {
        public string PushService { get; set; } = "";
        /// <summary>
        /// Gets or sets the SMS signature used in notifications.
        /// </summary>
        public string SmsSignature { get; set; } = "";
        /// <summary>
        /// Gets or sets the Email signature used in notifications.
        /// </summary>
        public string EmailSignature { get; set; } = "";
    }
}