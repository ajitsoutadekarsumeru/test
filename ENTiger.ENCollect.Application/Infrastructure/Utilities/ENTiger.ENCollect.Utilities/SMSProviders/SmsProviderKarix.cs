using Microsoft.Extensions.Logging;
using System.Reflection.PortableExecutable;
using System;

namespace ENTiger.ENCollect
{
    public class SmsProviderKarix : ISmsProvider
    {
        private readonly ILogger<SmsProviderKarix> _logger;
        private readonly IApiHelper _apiHelper;

        public SmsProviderKarix(ILogger<SmsProviderKarix> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<bool> SendSmsAsync(List<TenantSMSConfiguration> model, string numbers, string message, string file)
        {
            bool result = false;
            string smsURL = model.FirstOrDefault(x => x.Key.Equals("karixurl", StringComparison.OrdinalIgnoreCase))?.Value;
            string version = model.FirstOrDefault(x => x.Key.Equals("karixversion", StringComparison.OrdinalIgnoreCase))?.Value;
            string apiKey = model.FirstOrDefault(x => x.Key.Equals("karixapikey", StringComparison.OrdinalIgnoreCase))?.Value;
            string sender = model.FirstOrDefault(x => x.Key.Equals("karixsender", StringComparison.OrdinalIgnoreCase))?.Value;

            string stringPost = $"{smsURL}ver={version}&key={apiKey}&encrpt=0&dest={numbers}&send={sender}&text={message}";

            try
            {
                await LogToFileAsync(file, $"<---------------------Start--------------------->\nSMS Request  : {stringPost}\nSMS DATETIME : {DateTime.Now}\n*************************************************");

                var response = await _apiHelper.SendRequestAsync(null, stringPost, HttpMethod.Get);
                result = true;
                await LogToFileAsync(file, $"SMS Result   : {result}\nSMS Response : {response}\nSMS DATETIME : {DateTime.Now}\n<---------------------End--------------------->");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in SmsProviderKarix: SendSmsAsync");
                await LogToFileAsync(file, $"SMS Request   : {stringPost}\n***********************************************\nSMS Result    : {result}\nSMS Exception : {ex.Message}\nSMS DATETIME  : {DateTime.Now}\n<---------------------End--------------------->");
            }

            return result;
        }

        private async Task LogToFileAsync(string filePath, string content)
        {
            try
            {
                StreamWriter file1 = new StreamWriter(filePath, true);
                file1.WriteLine(content);
                file1.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in SmsProviderKarix: LogToFileAsync");
            }
        }
    }
}