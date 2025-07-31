using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.GeoTagModule;

/// <summary>
/// Handles sending email notifications after GeoReport generation.
/// </summary>
public sealed partial class SendEmailForGeoReportGenerated : ISendEmailForGeoReportBusGammaSubscriber
{
    private readonly ILogger<SendEmailForGeoReportGenerated> _logger;
    private readonly IRepoFactory _repoFactory;
    private readonly IEmailUtility _emailUtility;
    private readonly MessageTemplateFactory _messageTemplateFactory;
    private readonly CannedReportSetting _cannedReportSettings;
    private readonly IFileSystem _fileSystem;
    private readonly FilePathSettings _fileSettings;
    private FlexAppContextBridge? _flexAppContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailForGeoReportGenerated"/> class.
    /// </summary>
    public SendEmailForGeoReportGenerated(
        ILogger<SendEmailForGeoReportGenerated> logger,
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
    /// Executes the email notification process for GeoReport generation.
    /// </summary>
    public async Task Execute(GeoReportGenerated @event, IFlexServiceBusContext serviceBusContext)
    {
        _logger.LogInformation("GeoReport email dispatch started. File: {FileName}", $"{@event.FileName}.zip");

        try
        {
            InitializeContextAndRepo(@event);

            var tenantId = _flexAppContext?.TenantId;
            var template = _messageTemplateFactory.GeoReportGeneratedTemplate();
            var filePath = BuildAttachmentFilePath();
            var recipients = GetRecipientEmails();

            if (recipients.Count == 0)
            {
                _logger.LogWarning("No recipient emails configured.");
                return;
            }

            await SendEmailsAsync(recipients, tenantId, template, filePath, $"{@event.FileName}.zip" );

            _logger.LogInformation("GeoReport email dispatch completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending GeoReport email for File: {FileName}", @event.FileName);
            throw;
        }
    }

    /// <summary>
    /// Initializes the FlexAppContext and repository.
    /// </summary>
    private void InitializeContextAndRepo(GeoReportGenerated @event)
    {
        _flexAppContext = @event.AppContext;
        _logger.LogInformation("Repository initialized for Tenant: {TenantId}", _flexAppContext?.TenantId);
        _repoFactory.Init(@event);
    }

    /// <summary>
    /// Builds the file path of the GeoReport attachment.
    /// </summary>
    private string BuildAttachmentFilePath() =>
        _fileSystem.Path.Combine(
            _fileSettings.BasePath,
            _fileSettings.IncomingPath,
            _fileSettings.TemporaryPath
            );

    /// <summary>
    /// Parses the configured recipient emails.
    /// </summary>
    private List<string> GetRecipientEmails()
    {
        if (string.IsNullOrWhiteSpace(_cannedReportSettings.EmailIds))
        {
            _logger.LogWarning("Email configuration is empty.");
            return new();
        }

        var emails = _cannedReportSettings.EmailIds
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.Trim())
            .ToList();

        _logger.LogInformation("Total recipients: {Count}", emails?.Count);
        return emails;
    }

    /// <summary>
    /// Sends the email to all recipients with the GeoReport attachment.
    /// </summary>
    private async Task SendEmailsAsync(
        List<string> emails,
        string tenantId,
        IMessageTemplate template,
        string attachmentPath,
        string fileName)
    {
        List<string> files = [fileName];
        foreach (var email in emails)
        {
            try
            {
                _logger.LogInformation("Sending email to: {Email}", email);

                await _emailUtility.SendEmailAsync(
                    email,
                    template.EmailMessage,
                    template.EmailSubject,
                    tenantId,
                    files,
                    attachmentPath);

                _logger.LogInformation("Email sent to: {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to: {Email}", email);
            }
        }
    }
}
