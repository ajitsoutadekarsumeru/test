namespace ENTiger.ENCollect;

public class NotificationRequest
{
    /// <summary>
    /// The title of the notification (e.g., for mobile push notifications).
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The message body of the notification.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// The target recipient (could be a device token, user ID, topic, etc.).
    /// </summary>
    public string Recipient { get; set; }

    /// <summary>
    /// The type of push provider to use (e.g., "firebase", "onesignal").
    /// </summary>
    //public string ProviderType { get; set; }

    /// <summary>
    /// Optional: Custom metadata or data payload.
    /// </summary>
    public Dictionary<string, string>? Data { get; set; }

    /// <summary>
    /// Optional: Priority of the notification (e.g., high, normal).
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// Optional: Category or tag to help group notifications.
    /// </summary>
    public string? Category { get; set; }
}
