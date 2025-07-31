using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ENTiger.ENCollect
{
    public class SmsProviderInfoBip : ISmsProvider
    {
        protected readonly ILogger<SmsProviderInfoBip> _logger;
        private readonly IApiHelper _apiHelper;

        public SmsProviderInfoBip(ILogger<SmsProviderInfoBip> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }
        public async Task<bool> SendSmsAsync(List<TenantSMSConfiguration> model, string numbers, string message, string file)
        {
            bool result = false;

            string smsURL = model.Where(x => string.Equals(x.Key, "url")).Select(x => x.Value).FirstOrDefault();

            string apikey = model.Where(x => string.Equals(x.Key, "apikey")).Select(x => x.Value).FirstOrDefault();

            string sender = model.Where(x => string.Equals(x.Key, "sender")).Select(x => x.Value).FirstOrDefault();

            string code = model.Where(x => string.Equals(x.Key, "countrycode")).Select(x => x.Value).FirstOrDefault();
            string request = "{\"messages\":[{\"from\":\"" + sender + "\",\"destinations\":[{\"to\":\"" + code + numbers + "\"}],\"text\":\"" + message + "\"}]}";
            try
            {
                try
                {
                    StreamWriter file1 = new StreamWriter(file, true);
                    file1.WriteLine("<---------------------Start--------------------->");
                    file1.WriteLine("SMS Request  : " + request);
                    file1.WriteLine("SMS DATETIME : " + DateTime.Now.ToString());
                    file1.WriteLine("*************************************************");
                    file1.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in SmsUtility : SendInfoBipSMS - RequestToFile " + ex);
                }

                var headers = new Dictionary<string, string>
                {
                    { "App", apikey }
                };
                var response = await _apiHelper.SendRequestAsync(request, smsURL, HttpMethod.Post, headers);
                var responseContent = await response.Content.ReadAsStringAsync();
                result = true;
                try
                {
                    StreamWriter file2 = new StreamWriter(file, true);
                    file2.WriteLine("SMS Result   : " + result);
                    file2.WriteLine("SMS Response : " + responseContent);
                    file2.WriteLine("SMS DATETIME : " + DateTime.Now.ToString());
                    file2.WriteLine("<---------------------End--------------------->");
                    file2.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in SmsUtility : SendInfoBipSMS - ResponseToFile " + ex);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    StreamWriter file3 = new StreamWriter(file, true);
                    file3.WriteLine("SMS Request   : " + request);
                    file3.WriteLine("***********************************************");
                    file3.WriteLine("SMS Result    : " + result);
                    file3.WriteLine("SMS Exception : " + ex.Message);
                    file3.WriteLine("SMS DATETIME  : " + DateTime.Now.ToString());
                    file3.WriteLine("<---------------------End--------------------->");
                    file3.Close();
                }
                catch (Exception ex1)
                {
                    _logger.LogError("Exception in SmsUtility : SendInfoBipSMS - ExceptionToFile " + ex1);
                }
            }
            return result;
        }
    }
}