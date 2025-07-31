using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTiger.ENCollect.GeoTagModule;

/// <summary>
/// Generates the message template for the GeoReport notification.
/// </summary>
public class GeoReportFailedNotification : IMessageTemplate, IFlexUtilityService
{
    private readonly NotificationSettings _notificationSettings;

    /// <inheritdoc />
    public string EmailSubject { get; set; } = string.Empty;

    /// <inheritdoc />
    public string EmailMessage { get; set; } = string.Empty;

    /// <inheritdoc />
    public string SMSMessage { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of <see cref="GeoReportFailedNotification"/>.
    /// </summary>
    /// <param name="notificationSettings">The notification settings from configuration.</param>
    public GeoReportFailedNotification(IOptions<NotificationSettings> notificationSettings) =>
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
    The Geo Report generation process completed successfully, but no records were found for T-1 day ({DateTime.Now.AddDays(-1):dd-MMM-yyyy}).<br>
    Please note that no report is attached for this date.<br><br>
    Thanks & Regards,<br>
    {signature}<br>
    (This is a system generated email. Please do not reply back)<br>
    """;

    }
}
