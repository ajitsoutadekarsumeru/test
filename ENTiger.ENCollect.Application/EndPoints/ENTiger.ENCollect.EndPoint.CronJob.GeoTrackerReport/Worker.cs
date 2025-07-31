using CliWrap;
using CliWrap.Buffered;
using Sumeru.Flex;
using System.Net.Mail;
using System.Text;

namespace ENTiger.ENCollect
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var configuration = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>();
            string EmailFlag = configuration.GetSection("AppSettings")["EmailFlag"] != null ? configuration.GetSection("AppSettings")["EmailFlag"] : "no";
            string Autoclose = configuration.GetSection("AppSettings")["Autoclose"] != null ? configuration.GetSection("AppSettings")["Autoclose"] : "yes";
            try
            {             
                string SSISLogPath = configuration.GetSection("AppSettings")["SSISLogPath"];
                string ParamDatabaseConnection = configuration.GetSection("AppSettings")["ParamDatabaseConnection"];
                string ParamExcelDestination = configuration.GetSection("AppSettings")["ParamExcelDestination"];
                string PackageLocation = configuration.GetSection("AppSettings")["PackageLocation"];
                string PackageName = configuration.GetSection("AppSettings")["PackageName"];
                string dtExecPath = configuration.GetSection("AppSettings")["dtExecPath"];

                _logger.LogInformation("SSIS package start :");
                string PkgLocation = PackageLocation + PackageName + ".dtsx";

                _logger.LogInformation("PkgLocation: " + PkgLocation);
                _logger.LogInformation("ParamDatabaseConnection: " + ParamDatabaseConnection);
                _logger.LogInformation("ParamExcelDestination: " + ParamExcelDestination);
                _logger.LogInformation("PackageLocation: " + PackageLocation);
                _logger.LogInformation("SSISLogPath: " + SSISLogPath);


                string arguments = $"/f \"{PkgLocation}\" " +
                     $"/SET \"\\Package.Variables[$Package::ParamDatabaseConnection]\";\"{ParamDatabaseConnection}\" " +
                     $"/SET \"\\Package.Variables[$Package::ParamExcelDestination]\";\"{ParamExcelDestination}\" " +
                     $"/SET \"\\Package.Variables[$Package::ParamSSISLog]\";\"{SSISLogPath}\" ";
                _logger.LogInformation("dtExecPath: " + dtExecPath);
                _logger.LogInformation("arguments: " + arguments);
                try
                {
                    var stdOutBuffer = new StringBuilder();
                    var stdErrBuffer = new StringBuilder();
                    _logger.LogInformation("start to DTExe process");

                    var result = await Cli.Wrap(dtExecPath)
                .WithArguments([arguments])
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
                    _logger.LogInformation("End to DTExe process");

                    _logger.LogInformation("start to DTExe process");

                    // Output the results
                    _logger.LogInformation($"Standard Output: {result.StandardOutput}");
                    _logger.LogInformation($"Standard Error: {result.StandardError}");
                    _logger.LogInformation("EmailFlag : " + EmailFlag);
                    if (EmailFlag.ToLower() == "yes")
                    {
                        _logger.LogInformation("Email sending start");
                        SendUploadSuccessMail();
                    }

                    _logger.LogInformation("End to waiting output exit");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SSIS package Error :" + ex);
                    _logger.LogInformation($"An error occurred: {ex.Message}");
                    if (EmailFlag.ToLower() == "yes")
                    {
                        SendUploadFailMail();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SSIS package Error :" + ex);
                _logger.LogInformation($"An error occurred: {ex}");
                if (EmailFlag.ToLower() == "yes")
                {
                    SendUploadFailMail();
                }
            }

            _logger.LogInformation("Autoclose : " + Autoclose);
            if (Autoclose.ToLower() == "yes")
            {
                System.Environment.Exit(0);
            }
            await Task.CompletedTask;
        }

        public void SendUploadSuccessMail()
        {
            string subject = " ENCollect : Import account ";

            string msg =
               "<p>Dear Team,</p><br/>" +
               "<p> Account import into ENCollect is successfully done. " +
               //"and " + rowCount + " No.of accounts are processed.</p><br/>" +
               //"<p> " + rowCount + " accounts have been imported in ENCollect today.</p><br/>" +
               "<p>Regards<br/>" +
               "ENCollect Team <br/>" +
               "(This is a system generated email. Please do not reply back.)<br/>";
            SendMail(msg, subject);

        }

        public void SendUploadFailMail()
        {
            string subject = " ENCollect : Import account ";

            string msg =
                "<p>Dear Team,</p><br/>" +
                "<p> The daily import of the accounts in the ENCollect system has not been executed due to some error. Please check with admin for details.</p><br/>" +
                "<p>Regards<br/>" +
                "ENCollect Team <br/>" +
                "(This is a system generated email. Please do not reply back.)<br/>";
            SendMail(msg, subject);

        }
        public void SendMail(string msg, string Subject)
        {

            try
            {
                var configuration = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>();
                bool isProd = Convert.ToBoolean(configuration.GetSection("AppSettings")["IsProd"]);
                string ParamSourceFilePath = configuration.GetSection("AppSettings")["ParamSourceFilePath"];
                string Destination = configuration.GetSection("AppSettings")["Destination"];
                string ParamSmtpUser = configuration.GetSection("AppSettings")["ParamSmtpUser"];
                string ParamSmtpPassword = configuration.GetSection("AppSettings")["ParamSmtpPassword"];
                string ParamSmtpServer = configuration.GetSection("AppSettings")["ParamSmtpServer"];
                string ParamEnableSsl = configuration.GetSection("AppSettings")["ParamEnableSsl"];
                string ParamSmtpPort = configuration.GetSection("AppSettings")["ParamSmtpPort"];
                string ParamMailFrom = configuration.GetSection("AppSettings")["ParamMailFrom"];
                string ParamToEmailAddress = configuration.GetSection("AppSettings")["ParamToEmailAddress"];
                string ParamCCEmailAddress = configuration.GetSection("AppSettings")["ParamCCEmailAddress"];


                MailMessage mailMsg = new MailMessage();
                foreach (var address in ParamToEmailAddress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMsg.To.Add(address);
                }
                foreach (var address in ParamCCEmailAddress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMsg.CC.Add(address);
                }

                mailMsg.From = new MailAddress(ParamMailFrom);
                // Subject and Body
                mailMsg.Subject = Subject;
                StringBuilder sb = new StringBuilder();
                string body = string.Empty;
                sb.Append("<html>");
                sb.Append("<table>");
                sb.Append("<tr><td>&nbsp;</td></tr>");
                sb.Append("<tr><td>" + msg + "</td></tr>");
                sb.Append("<tr><td>&nbsp;</td></tr>");
                sb.Append("</table>");
                sb.Append("</html>");
                body = sb.ToString();
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(ParamSmtpServer, Convert.ToInt32(ParamSmtpPort));
                smtpClient.EnableSsl = Convert.ToBoolean(ParamEnableSsl);

                if (!isProd)
                {
                    System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(ParamSmtpUser, ParamSmtpPassword);
                    smtpClient.Credentials = credentials;
                }
                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                //TODO: loger
                _logger.LogError("Mail Error : " + ex.Message);
            }
        }
    }
}