using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayRazorPay : IPaymentGateway
    {
        protected readonly ILogger<PaymentGatewayRazorPay> _logger;
        protected string _tenantId = string.Empty;
        protected string? url = string.Empty;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly IApiHelper _apiHelper;

        public PaymentGatewayRazorPay(ILogger<PaymentGatewayRazorPay> logger, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, IApiHelper apiHelper)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<PaymentTransactionDto> GetPaymentLinkDetails(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails, string tenantId)
        {
            PaymentTransactionDto? transaction = null;
            _logger.LogInformation("PaymentGatewayRazorPay : GetPaymentLinkDetails - Start");
            var config = paymentDetails.ToDictionary(x => x.Parameter.ToLowerInvariant(), x => x.Value);
            string url = config.GetValueOrDefault("razorpayurl") ?? "";
            string UserName = config.GetValueOrDefault("razorpayusername") ?? "";
            string Password = config.GetValueOrDefault("razorpaypassword") ?? "";
            string logPath = config.GetValueOrDefault("razorpaylogfilepath") ?? "";
            string? filePath = GetFilePath(logPath);

            dynamic result = null;
            string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{UserName}:{Password}"));


            var input = GetRazorPayInput(collection, paymentDetails);

            try
            {
                StreamWriter file = new StreamWriter(filePath, true);
                file.WriteLine("<---------------------Start--------------------->");
                file.WriteLine("Plugin Name     : OnlinePaymentNotificationService");
                file.WriteLine("Url             : " + url);
                file.WriteLine("razorpayusername             : " + UserName);
                file.WriteLine("razorpaypassword             : " + Password);
                file.WriteLine("Authorization   : " + authorization);
                file.WriteLine("Request Data    : " + JsonConvert.SerializeObject(input));
                file.WriteLine("Request DATETIME: " + DateTime.Now.ToString());
                file.WriteLine("***************************************************");
                file.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in OnlinePaymentNotificationService : Request File - " + ex);
            }
            var responseString = string.Empty;
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Basic {authorization}" }
            };
            _logger.LogInformation("Authorization header being sent: Basic {Authorization}", authorization);

            try
            {
                _logger.LogInformation("OnlinePaymentNotificationService : API Initiated");

                ByteArrayContent byteContent = getDataContent(input);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string contentString = await byteContent.ReadAsStringAsync();

                var response = await _apiHelper.SendRequestAsync(contentString, url, HttpMethod.Post, headers);
                if (!response.IsSuccessStatusCode)
                {
                    try
                    {
                        _logger.LogInformation("OnlinePaymentNotificationService : API Response : Failed StatusCode - " + response.StatusCode);
                        responseString = response.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject(responseString);
                        string message = response.Content.ReadAsStringAsync().Result;
                        _logger.LogInformation("OnlinePaymentService : Error - " + message + "ReasonPhrase - " + response?.ReasonPhrase);
                        StreamWriter file = new StreamWriter(filePath, true);
                        file.WriteLine("Failed StatusCode   : " + response?.StatusCode);
                        file.WriteLine("Error               : " + response?.Content.ReadAsStringAsync().Result);
                        file.WriteLine("ReasonPhrase        : " + response?.ReasonPhrase);
                        file.WriteLine("Response DATETIME   : " + DateTime.Now.ToString());
                        file.WriteLine("<---------------------End--------------------->");
                        file.Close();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception in OnlinePaymentNotificationService : Failed Status - " + ex);
                    }
                }
                else
                {
                    try
                    {
                        _logger.LogInformation("OnlinePaymentNotificationService : API Response  : Success StatusCode -  " + response.StatusCode);
                        responseString = response.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject(responseString);
                        _logger.LogInformation("OnlinePaymentService : Result - " + JsonConvert.DeserializeObject(responseString));
                        StreamWriter file = new StreamWriter(filePath, true);
                        file.WriteLine("Success StatusCode  : " + response.StatusCode);
                        file.WriteLine("Response            : " + result);
                        file.WriteLine("Response DATETIME   : " + DateTime.Now.ToString());
                        file.WriteLine("<---------------------End--------------------->");
                        file.Close();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception in OnlinePaymentNotificationService : Success Status - " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex + ex?.Message + ex?.StackTrace + ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace;
                _logger.LogInformation("Exception in OnlinePaymentNotificationService : GetRazorPayPaymentLinkDetails - " + message);
                StreamWriter file = new StreamWriter(filePath, true);
                file.WriteLine("Exception           : " + message);
                file.WriteLine("Response DATETIME   : " + DateTime.Now.ToString());
                file.WriteLine("<---------------------End--------------------->");
                file.Close();
            }

            _logger.LogInformation("OnlinePaymentNotificationService : GetRazorPayPaymentLinkDetails - End");

            if (result != null && result.status == "created")
            {
                transaction = new PaymentTransactionDto();
                transaction.MerchantReferenceNumber = result.reference_id;
                transaction.ResponseMessage = JsonConvert.SerializeObject(result);
                transaction.Status = result.status;
                transaction.Amount = Convert.ToDecimal(result.amount);
                transaction.Currency = result.currency;

                PaymentGatewayDto gateway = new PaymentGatewayDto();
                gateway.MerchantId = result.id;
                gateway.PostURL = url;
                gateway.ReturnURL = result.short_url;

                transaction.PaymentGateway = gateway;
            }
            return transaction;
        }

        public async Task SendPaymentLinkEmail(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId result, string tenantId)
        {
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.RazorPayOnlinePaymentTemplate(collectionDto, result, tenantId);

            //Send Email
            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
        }

        public async Task SendPaymentLinkSMS(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId result, string tenantId)
        {
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.RazorPayOnlinePaymentTemplate(collectionDto, result, tenantId);

            //Send SMS
            await _smsUtility.SendSMS(collectionDto.MobileNo, messageTemplate.SMSMessage, tenantId);
        }

        #region Private

        private ByteArrayContent getDataContent(object data)
        {
            _logger.LogInformation("OnlinePaymentNotificationService : getDataContent - Start");
            return new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        private object GetRazorPayInput(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails)
        {
            _logger.LogInformation("OnlinePaymentNotificationService : GetRazorPayInput - Start");
            var config = paymentDetails.ToDictionary(x => x.Parameter.ToLowerInvariant(), x => x.Value);

            string? currency = config.GetValueOrDefault("razorpaycurrency");
            string? description = config.GetValueOrDefault("razoraydescription");
            string? countrycode = config.GetValueOrDefault("razorpaycountrycode");
            bool sms = bool.TryParse(config.GetValueOrDefault("razorpaysms"), out var smsVal) && smsVal;
            bool email = bool.TryParse(config.GetValueOrDefault("razorpayemail"), out var emailVal) && emailVal;
            bool reminder = bool.TryParse(config.GetValueOrDefault("razorpayreminder"), out var reminderVal) && reminderVal;
            string? notes = config.GetValueOrDefault("razorpaynotes");
            double timeInMins = double.TryParse(config.GetValueOrDefault("razorpaytimeinmins"), out var timeVal) ? timeVal : 1440.0;

            var datetime = DateTimeOffset.Now.ToUnixTimeSeconds();
            TimeSpan t = DateTime.Now.AddMinutes(timeInMins) - new DateTime(1970, 1, 1);
            string epochTime = t.TotalSeconds.ToString("#");
            var request = new
            {
                amount = collection.Amount * 100,
                currency = currency,
                expire_by = Convert.ToDecimal(epochTime),
                reference_id = collection.CustomId,
                description = description,
                customer = new
                {
                    name = collection.CustomerName,
                    contact = countrycode + collection.MobileNo,
                    email = collection.EMailId
                },
                notify = new
                {
                    sms = sms,
                    email = email
                },
                reminder_enable = reminder,
                notes = new
                {
                    policy_name = notes
                }
            };
            _logger.LogInformation("OnlinePaymentNotificationService : GetRazorPayInput - End | JSON - " + JsonConvert.SerializeObject(request));
            return request;
        }

        private string GetFilePath(string? logPath)
        {
            _logger.LogInformation("PaymentGatewayRazorPay - GetPaymentLinkDetails : GetFilePath - Start | LogPath - " + logPath);
            string filePath;
            string logname = "ServiceLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (string.IsNullOrEmpty(logPath))
            {
                filePath = logname;
            }
            else
            {
                string logFilePath = Path.Combine(logPath, _tenantId);
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                filePath = Path.Combine(logFilePath, logname);
            }
            _logger.LogInformation("PaymentGatewayRazorPay - GetPaymentLinkDetails : GetFilePath - End | FilePath - " + filePath);
            return filePath;
        }

        #endregion Private
    }
}