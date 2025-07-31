using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.GeoTagModule;

/// <summary>
/// Handles sending email notifications after GeoReport generation failures.
/// </summary>
public sealed class SendEmailForGeoReportFailed : ISendEmailForGeoReportFailedBusGammaSubscriber
{
    private readonly ILogger<SendEmailForGeoReportFailed> _logger;
    private readonly IRepoFactory _repoFactory;
    private readonly IEmailUtility _emailUtility;
    private readonly MessageTemplateFactory _messageTemplateFactory;
    private readonly CannedReportSetting _cannedReportSettings;
    private readonly IFileSystem _fileSystem;
    private readonly FilePathSettings _fileSettings;

    private FlexAppContextBridge? _flexAppContext;

    public SendEmailForGeoReportFailed(
        ILogger<SendEmailForGeoReportFailed> logger,
        IRepoFactory repoFactory,
        IEmailUtility emailUtility,
        MessageTemplateFactory messageTemplateFactory,
        IOptions<CannedReportSetting> cannedReportSettings,
        IFileSystem fileSystem,
        IOptions<FilePathSettings> fileSettings)
    {
        _logger = logger;
        _repoFactory = repoFactory;
        _emailUtility = emailUtility;
        _messageTemplateFactory = messageTemplateFactory;
        _cannedReportSettings = cannedReportSettings.Value;
        _fileSystem = fileSystem;
        _fileSettings = fileSettings.Value;
    }

    /// <summary>
    /// Executes the email notification process for GeoReport generation failure.
    /// </summary>
    public async Task Execute(GeoReportFailed @event, IFlexServiceBusContext serviceBusContext)
    {
        _logger.LogInformation("GeoReport email dispatch started");

        try
        {
            InitializeContextAndRepository(@event);

            var recipientEmails = GetRecipientEmails();
            if (recipientEmails.Count == 0)
            {
                _logger.LogWarning("No recipient emails configured.");
                return;
            }

            var template = _messageTemplateFactory.GeoReportFailedTemplate();

            await SendEmailsAsync(recipientEmails, _flexAppContext?.TenantId ?? string.Empty, template);

            _logger.LogInformation("GeoReport email dispatch completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending GeoReport failure email.");
            throw;
        }
    }

    /// <summary>
    /// Initializes the application context and repository factory.
    /// </summary>
    private void InitializeContextAndRepository(GeoReportFailed @event)
    {
        _flexAppContext = @event.AppContext;
        _repoFactory.Init(@event);
        _logger.LogInformation("Repository initialized for Tenant: {TenantId}", _flexAppContext?.TenantId);
    }

    /// <summary>
    /// Retrieves recipient email addresses from configuration.
    /// </summary>
    private List<string> GetRecipientEmails()
    {
        if (string.IsNullOrWhiteSpace(_cannedReportSettings.EmailIds))
        {
            _logger.LogWarning("Configured email list is empty.");
            return new();
        }

        var emails = _cannedReportSettings.EmailIds
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(email => email.Trim())
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .ToList();

        _logger.LogInformation("Total recipient(s): {Count}", emails.Count);
        return emails;
    }

    /// <summary>
    /// Sends email notifications to all configured recipients.
    /// </summary>
    private async Task SendEmailsAsync(List<string> emails, string tenantId, IMessageTemplate template)
    {
        foreach (var email in emails)
        {
            try
            {
                _logger.LogInformation("Sending email to: {Email}", email);

                await _emailUtility.SendEmailAsync(
                    email,
                    template.EmailMessage,
                    template.EmailSubject,
                    tenantId);

                _logger.LogInformation("Email successfully sent to: {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to: {Email}", email);
            }
        }
    }
}
