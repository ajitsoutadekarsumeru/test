using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ENTiger.ENCollect
{
    public class SmsProvider24X7 : ISmsProvider
    {
        protected readonly ILogger<SmsProvider24X7> _logger;
        public SmsProvider24X7(ILogger<SmsProvider24X7> logger)
        {
            _logger = logger;
        }
        public async Task<bool> SendSmsAsync(List<TenantSMSConfiguration> model, string numbers, string message, string file)
        {
            bool result = false;
            string smsURL = GetModelValue(model, "url");
            string usernameLabel = GetModelValue(model, "usernamelabel");
            string usernameValue = GetModelValue(model, "usernamevalue");
            string PasswordLabel = GetModelValue(model, "passlabel");
            string PasswordValue = GetModelValue(model, "passvalue");
            string ToLabel = GetModelValue(model, "tolabel");
            string MessageLabel = GetModelValue(model, "messagelabel");
            string FromLabel = GetModelValue(model, "fromlabel");
            string Fromvalue = GetModelValue(model, "fromvalue");
            string signature = GetModelValue(model, "signature");

            numbers = numbers.TrimStart(',');
            message = Regex.Replace(message, "<.*?>", "");
            message = message.Replace("signature", signature);

            int length = numbers.Trim().Length;
            if (length > 10)
            {
                PasswordValue = model.Where(x => string.Equals(x.Key, "internationalpassvalue")).Select(x => x.Value).FirstOrDefault();
            }
            else
            {
                PasswordValue = model.Where(x => string.Equals(x.Key, "nationalpassvalue")).Select(x => x.Value).FirstOrDefault();
            }

            string to = HttpUtility.UrlEncode(numbers);
            string text = HttpUtility.UrlEncode(message);

            string stringpost = smsURL + FromLabel + "=" + Fromvalue + "&" +
                                usernameLabel + "=" + usernameValue + "&" + PasswordLabel + "=" + PasswordValue + "&" +
                                ToLabel + "=" + to + "&" + MessageLabel + "=" + text;

            string SmsData = "";
            foreach (var obj in model.Where(x => x.Key.StartsWith("smsdata")))
            {
                string s = obj.Key.Substring(7);
                SmsData = SmsData + "&" + s + "=" + obj.Value;
            }

            stringpost += SmsData;
            string strResponse = string.Empty;

            //As per VAPT point added this 
            string SMSmsg = stringpost;
            string maksmobileno = MaskNumberFunc(to);
            SMSmsg = SMSmsg.Replace(to, maksmobileno);
            try
            {
                try
                {
                    StreamWriter file1 = new StreamWriter(file, true);
                    file1.WriteLine("<---------------------Start--------------------->");
                    file1.WriteLine("SMS Request  : " + SMSmsg);
                    file1.WriteLine("SMS DATETIME : " + DateTime.Now.ToString());
                    file1.WriteLine("*************************************************");
                    file1.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in SmsUtility : Send24_7SMS - RequestToFile " + ex);
                }

                WebRequest myWebRequest = WebRequest.Create(stringpost);
                WebResponse myWebResponse = myWebRequest.GetResponse();
                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                strResponse = readStream.ReadToEnd();
                result = true;
                try
                {
                    StreamWriter file2 = new StreamWriter(file, true);
                    file2.WriteLine("SMS Result   : " + result);
                    file2.WriteLine("SMS Response : " + strResponse);
                    file2.WriteLine("SMS DATETIME : " + DateTime.Now.ToString());
                    file2.WriteLine("<---------------------End--------------------->");
                    file2.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in SmsUtility : Send24_7SMS - ResponseToFile " + ex);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    StreamWriter file3 = new StreamWriter(file, true);
                    file3.WriteLine("SMS Request   : " + SMSmsg);
                    file3.WriteLine("***********************************************");
                    file3.WriteLine("SMS Result    : " + result);
                    file3.WriteLine("SMS Exception : " + ex.Message);
                    file3.WriteLine("SMS DATETIME  : " + DateTime.Now.ToString());
                    file3.WriteLine("<---------------------End--------------------->");
                    file3.Close();
                }
                catch (Exception ex1)
                {
                    _logger.LogError("Exception in SmsUtility : Send24_7SMS - ExceptionToFile " + ex1);
                }
            }
            return result;
        }
        private string GetModelValue(List<TenantSMSConfiguration> model, string key, string defaultValue = null)
        {
            var value = model
                .Where(x => string.Equals(x.Key, key))
                .Select(x => x.Value)
                .FirstOrDefault();
            return value ?? defaultValue;
        }

        public string MaskNumberFunc(string MaskNumber)
        {
            _logger.LogDebug("MaskNumber :" + MaskNumber);
            try
            {
                if (!string.IsNullOrEmpty(MaskNumber))
                {
                    var firstDigits = MaskNumber.Substring(0, 0);
                    var lastDigits = MaskNumber.Substring(MaskNumber.Length - 4, 4);

                    var requiredMask = new String('X', MaskNumber.Length - firstDigits.Length - lastDigits.Length);

                    var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                    var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    _logger.LogDebug(maskedCardNumberWithSpaces);
                    return maskedCardNumberWithSpaces;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in SmsUtility : MaskNumberFunc " + ex);
                return string.Empty;
            }
        }
    }
}