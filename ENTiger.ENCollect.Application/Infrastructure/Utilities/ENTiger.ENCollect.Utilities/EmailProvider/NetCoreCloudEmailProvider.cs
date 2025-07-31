using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace ENTiger.ENCollect
{
    public class EmailProviderNetCoreCloud : IEmailProvider
    {
        protected readonly ILogger<EmailProviderNetCoreCloud> _logger;
        private readonly RestClientSettings _restClientSettings;
        private readonly IApiHelper _apiHelper;

        public EmailProviderNetCoreCloud(ILogger<EmailProviderNetCoreCloud> logger, IOptions<RestClientSettings> restClientSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _restClientSettings = restClientSettings.Value;
            _apiHelper = apiHelper;
        }

        public async Task<bool> SendEmailAsync(TenantEmailConfiguration model, string toAddress, string msg, string subject, string logFilePath, List<string>? files = null, string? filePath = null, bool includedSignature = false)
        {
            bool result = false;
            var emailNetCloudApiUrl = _restClientSettings.NetCoreCloudApiUrl;

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            string logFile = Path.Combine(logFilePath, $"EmailLog_{DateTime.Now:yyyyMMdd}.txt");
            string emailLog = $"Email Log : {DateTime.Now:dd-MM-yyyy}{Environment.NewLine}";

            emailLog += $"{Environment.NewLine}< ------------------Email Request Start ---------------- >";
            emailLog += $"{Environment.NewLine}Request DateTime : {DateTime.Now}";
            emailLog += $"{Environment.NewLine}NetCoreCloudApiUrl : {emailNetCloudApiUrl}";
            var body = BuildEmailBody(msg, includedSignature, model.MailSignature);
            try
            {
                var emailPayload = CreateEmailPayload(toAddress, subject, body, model.MailFrom, files, filePath);
                var jsonContent = JsonConvert.SerializeObject(emailPayload);
                
                emailLog += $"{Environment.NewLine}Request Json : {jsonContent}";
                await LogToFileAsync(logFile, emailLog, true);

                var headers = new Dictionary<string, string>
                {
                    { "api_key", _restClientSettings.NetCoreCloudApiKey }
                };
                var response = await _apiHelper.SendRequestAsync(jsonContent, emailNetCloudApiUrl, HttpMethod.Post, headers);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Email sent successfully to {Recipient}", toAddress);
                    await LogToFileAsync(logFile, responseBody, true);
                    result = true;
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to send email. Status code: {StatusCode}, Response: {ResponseBody}", response.StatusCode, responseBody);
                    await LogToFileAsync(logFile, $"Response: Failed to send email. Status code: {response.StatusCode}, Response: {responseBody}", true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while sending email");
                await LogToFileAsync(logFile, $"Exception: {ex}", true);
            }
            finally
            {
                _logger.LogInformation("Email request completed for {Recipient}", toAddress);
            }

            return result;
        }

        private static string BuildEmailBody(string msg, bool includedSignature, string mailSignature)
        {
            var sb = new StringBuilder();
            sb.Append("<html><table><tr><td>&nbsp;</td></tr>")
              .Append($"<tr><td>{msg}</td></tr><tr><td>&nbsp;</td></tr>");

            if (!includedSignature)
            {
                sb.Append("<tr><td>Regards,</td></tr>")
                  .Append($"<tr><td>{mailSignature}</td></tr>");
            }

            sb.Append("</table></html>");
            return sb.ToString();
        }

        private static async Task LogToFileAsync(string filePath, string logContent, bool appendEndMarker = false)
        {
            await using var fileWriter = new StreamWriter(filePath, true);
            await fileWriter.WriteLineAsync(logContent);

            if (appendEndMarker)
            {
                await fileWriter.WriteLineAsync("< -------------------Email Request End ---------------- >");
            }
        }

        private static object CreateEmailPayload(string toAddress, string subject, string body, string mailFrom, List<string>? files = null, string? filePath = null)
        {
            var attachments = new List<object>();

            if (files != null && filePath != null)
            {
                foreach (var fileName in files)
                {
                    string fullPath = Path.Combine(filePath, fileName);

                    if (File.Exists(fullPath))
                    {
                        byte[] fileBytes = File.ReadAllBytes(fullPath);
                        string base64Content = Convert.ToBase64String(fileBytes);

                        attachments.Add(new
                        {
                            name = fileName,
                            content = base64Content
                        });
                    }
                }
            }

            return new
            {
                from = new { email = mailFrom },
                subject = subject,
                template_id = 0,
                content = new[] { new { type = "html", value = body } },
                personalizations = new[]
                {
                new
                {
                    to = new[] { new { email = toAddress } },
                    cc = Array.Empty<object>(),
                    bcc = Array.Empty<object>(),
                    attachments = attachments.ToArray()
                }
            },
                schedule = 0
            };
        }
    }
}