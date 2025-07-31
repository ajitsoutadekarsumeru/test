using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Text;

namespace ENTiger.ENCollect
{
    public class EmailProviderSmtp : IEmailProvider
    {
        protected readonly ILogger<EmailProviderSmtp> _logger;
        public EmailProviderSmtp(ILogger<EmailProviderSmtp> logger)
        {
            _logger = logger;
        }
        public async Task<bool> SendEmailAsync(TenantEmailConfiguration model, string toAddress, string msg, string subject, string logFilePath, List<string>? files = null, string? filePath = null, bool includedSignature = false)
        {
            bool result = false;
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }
            string EmailLog = string.Empty;
            string logname = "EmailLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string file = Path.Combine(logFilePath, logname);
            if (!File.Exists(file))
            {
                EmailLog = ("Email Log : " + DateTime.Now.ToString("dd-MM-yyyy") + Environment.NewLine);
            }
            EmailLog += Environment.NewLine + "< ------------------Email Request Start ---------------- >";

            try
            {
                EmailLog += Environment.NewLine + "Request DateTime : " + DateTime.Now.ToString();

                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(model.MailFrom);
                EmailLog += Environment.NewLine + "MailFrom : " + model.MailFrom;

                foreach (var address in toAddress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress EmailAddress = new MailAddress(address);
                    mailMsg.To.Add(EmailAddress);
                }
                EmailLog += Environment.NewLine + "ToEmail  : " + toAddress;

                _logger.LogInformation("EmailUtility : FilePath - " + filePath + " | Files Count - " + files?.Count());
                if (files != null && files.Count > 0 && !string.IsNullOrEmpty(filePath))
                {
                    foreach (var doc in files)
                    {
                        string path = Path.Combine(filePath, doc);
                        _logger.LogInformation("EmailUtility : Attach File - " + path);
                        if (File.Exists(path))
                        {
                            EmailLog += Environment.NewLine + "File     : " + path;
                            mailMsg.Attachments.Add(new Attachment(path));
                        }
                    }
                }
                mailMsg.Subject = subject;
                EmailLog += Environment.NewLine + "Subject  : " + mailMsg.Subject;

                StringBuilder sb = new StringBuilder();
                string body = string.Empty;
                sb.Append("<html>");
                sb.Append("<table>");
                sb.Append("<tr><td>&nbsp;</td></tr>");
                sb.Append("<tr><td>" + msg + "</td></tr>");
                sb.Append("<tr><td>&nbsp;</td></tr>");
                if (!includedSignature)
                {
                    sb.Append("<tr><td>Regards,</td></tr>");
                    sb.Append("<tr><td>" + model.MailSignature + "</td></tr>");
                }
                sb.Append("</table>");
                sb.Append("</html>");
                body = sb.ToString();

                mailMsg.Body = body;
                EmailLog += Environment.NewLine + "Body     : " + mailMsg.Body + Environment.NewLine;

                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(model.SmtpServer, Convert.ToInt32(model.SmtpPort));
                smtpClient.EnableSsl = Convert.ToBoolean(model.EnableSsl);

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(model.SmtpUser, model.SmtpPassword);
                smtpClient.Credentials = credentials;
                try
                {
                    using (StreamWriter file1 = new StreamWriter(file, true))
                    {
                        file1.WriteLine(EmailLog);
                        file1.WriteLine("< ----------------------------------------------------- >");
                        file1.Close();
                    }
                    await smtpClient.SendMailAsync(mailMsg);
                    result = true;
                    _logger.LogInformation("EmailUtility : SendMail - Sent Successfully");
                    using (StreamWriter file2 = new StreamWriter(file, true))
                    {
                        file2.WriteLine("Response DateTime :" + DateTime.Now.ToString());
                        file2.WriteLine("Response : Email Successfully Sent");
                        file2.WriteLine("< -------------------Email Request End ---------------- >");
                        file2.Close();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in EmailUtility : SendMail - Send : " + ex);
                    using (StreamWriter file2 = new StreamWriter(file, true))
                    {
                        file2.WriteLine("Response DateTime  : " + DateTime.Now.ToString());
                        file2.WriteLine("Response Exception : " + ex);
                        file2.WriteLine(" < ------------------Email Request End ---------------- >");
                        file2.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in EmailUtility : SendMail - " + ex);
            }
            return result;
        }
    }
}