using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule;

/// <summary>
/// Generates the message template for the GeoReport notification.
/// </summary>
public sealed class GeoReportGeneratedNotification : IMessageTemplate, IFlexUtilityService
{
    private readonly NotificationSettings _notificationSettings;

    /// <inheritdoc />
    public string EmailSubject { get; set; } = string.Empty;

    /// <inheritdoc />
    public string EmailMessage { get; set; } = string.Empty;

    /// <inheritdoc />
    public string SMSMessage { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of <see cref="GeoReportGeneratedNotification"/>.
    /// </summary>
    /// <param name="notificationSettings">The notification settings from configuration.</param>
    public GeoReportGeneratedNotification(IOptions<NotificationSettings> notificationSettings) =>
        _notificationSettings = notificationSettings.Value;

    /// <summary>
    /// Constructs the subject and body of the email message.
    /// </summary>
    public void ConstructData()
    {
        var signature = _notificationSettings.EmailSignature;
        var reportDate = DateTime.Now.Date.AddDays(-1);

        EmailSubject = $"Geo Report (Canned) – {reportDate:dd-MM-yyyy}";
        EmailMessage = $"""
            Dear Team,<br><br>
            Please find the attached for the report.<br><br>
            Thanks & Regards,<br>
            {signature}<br>
            (This is a system generated email. Please do not reply back)<br>
            """;
    }
}
