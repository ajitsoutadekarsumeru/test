using Microsoft.Extensions.Logging;
using System.Text;
using System.Xml;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayPaynimo : IPaymentGateway
    {
        protected readonly ILogger<PaymentGatewayPaynimo> _logger;
        protected string _tenantId = string.Empty;
        protected string? url = string.Empty;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        private readonly IApiHelper _apiHelper;
        private readonly HashSet<string> _allowedDomains = new HashSet<string>
        {
            "trusted-domain1.com",
            "trusted-domain2.com",
            "bankapis.com"
        };
        public PaymentGatewayPaynimo(ILogger<PaymentGatewayPaynimo> logger, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, IApiHelper apiHelper)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _apiHelper = apiHelper;
        }
        public async Task SendPaymentLinkEmail(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId result, string tenantId)
        {
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.PaynimoOnlinePaymentTemplate(collectionDto, result, tenantId);

            //Send Email
            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
        }

        public async Task SendPaymentLinkSMS(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId result, string tenantId)
        {
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.PaynimoOnlinePaymentTemplate(collectionDto, result, tenantId);

            //Send SMS
            await _smsUtility.SendSMS(collectionDto.MobileNo, messageTemplate.SMSMessage, tenantId);
        }
        public async Task<PaymentTransactionDto> GetPaymentLinkDetails(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails, string tenantId)
        {
            PaymentTransactionDto? transaction = null;
            string result = string.Empty;
            _tenantId = tenantId;

            _logger.LogInformation("PaymentGatewayPaynimo : GetPaymentLinkDetails - Start");

            string? url = paymentDetails
                .Where(x => x.Parameter.Contains("url"))
                .Select(x => x.Value)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("Payment URL is missing in paymentDetails.");
                throw new InvalidOperationException("Payment URL is required.");
            }

            string? logPath = paymentDetails
                .Where(x => x.Parameter.Contains("logfilepath"))
                .Select(x => x.Value)
                .FirstOrDefault();

            string filePath = GetFilePath(logPath);
            string input = GetInput(collection, paymentDetails, filePath);
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(input);
            HttpResponseMessage response = await SendHttpRequest(url, soapEnvelopeXml);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"HTTP request failed with status code: {response.StatusCode}");
                throw new HttpRequestException($"Failed to get response from {url}");
            }

            result = await response.Content.ReadAsStringAsync();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            result = xml.InnerText;

            await File.AppendAllTextAsync(filePath, $"\\\\\\\\\\\\\\\\\\\\\\\\\\\nCBS Response: {result}\nCBS DATE TIME: {DateTime.Now}\n");

            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : Response - " + result);

            var values = result.Split('|');
            if (values.Length >= 26)
            {
                transaction = new PaymentTransactionDto
                {
                    MerchantReferenceNumber = values[22],
                    ResponseMessage = result,
                    StatusCode = values[24],
                    Status = values[25],
                    Amount = Convert.ToDecimal(values[4]),
                    PaymentGateway = new PaymentGatewayDto
                    {
                        Name = values[7],
                        MerchantId = values[1],
                        MerchantKey = "",
                        PostURL = url,
                        ReturnURL = values[23]
                    }
                };
            }

            return transaction;
        }

        #region Private
        private async Task<HttpResponseMessage> SendHttpRequest(string url, XmlDocument soapEnvelopeXml)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                _logger.LogError($"Invalid URL format: {url}");
                throw new ArgumentException("Invalid URL format", nameof(url));
            }

            if (!IsAllowedDomain(uri))
            {
                _logger.LogError($"Unauthorized domain: {uri.Host}");
                throw new UnauthorizedAccessException("Requests to this domain are not allowed.");
            }

            _logger.LogInformation($"Sending request to {uri}");

            var content = new StringContent(soapEnvelopeXml.OuterXml, Encoding.UTF8, "text/xml");
            string contentString = await content.ReadAsStringAsync();
            return await _apiHelper.SendRequestAsync(contentString, uri.AbsoluteUri, HttpMethod.Post);
        }

        private bool IsAllowedDomain(Uri uri)
        {
            string host = uri.Host.ToLowerInvariant();
            return _allowedDomains.Contains(host);
        }
        private XmlDocument CreateSoapEnvelope(string input)
        {
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : CreateSoapEnvelope - Start");

            XmlDocument soapEnvelopeXml = new XmlDocument();
            string xml = "<x:Envelope xmlns:x=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">" +
                         "<x:Header/>" +
                             "<x:Body>" +
                                 "<tem:RPPServiceCall>" +
                                     "<tem:RPPRequest>" +
                                         input +
                                     "</tem:RPPRequest>" +
                                 "</tem:RPPServiceCall>" +
                             "</x:Body>" +
                         "</x:Envelope>";

            _logger.LogInformation("SOAP Request: " + xml);

            soapEnvelopeXml.LoadXml(xml);
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : CreateSoapEnvelope - End");
            return soapEnvelopeXml;
        }
        private string GetInput(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails, string filePath)
        {
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : GetInput - Start");
            string request;
            // Helper function to get value based on parameter name
            string? GetParameterValue(string parameterName) =>
                paymentDetails
                    .Where(x => x.Parameter.Contains(parameterName))
                    .Select(x => x.Value)
                    .FirstOrDefault();

            string? RPPVersion = GetParameterValue("rrpversion");
            string? MerchantId = GetParameterValue("merchantid");
            string? MerchantName = GetParameterValue("merchantname");
            string? MerchantEmail = GetParameterValue("merchantemail");
            string? MerchantMobile = GetParameterValue("merchantmobile");
            string? MerchantEmailorMobile = GetParameterValue("merchantemailormobile");
            string? NoteFromMerchant = GetParameterValue("notefrommerchant");
            string? DistributorIdentifier = GetParameterValue("distributoridentifier");
            string? CustomerEmailorMobile = GetParameterValue("cutomeremailormobile");

            // Assign MerchantEmailorMobile based on condition
            MerchantEmailorMobile = string.Equals(MerchantEmailorMobile, "email") ? MerchantEmail : MerchantMobile;

            // Assign CustomerEmailorMobile based on condition
            CustomerEmailorMobile = string.Equals(CustomerEmailorMobile, "email") ? collection.EMailId : collection.MobileNo;

            string? CustomerName = collection.CustomerName;
            string? TotalAmount = collection.Amount?.ToString();
            string? InvoiceNo = collection.CustomId;
            string? CustomerId = collection?.Account?.CUSTOMERID;
            string? Product = collection?.Account?.PRODUCT;
            string? AgentMobileNumber = collection?.Collector?.PrimaryMobileNumber;

            request = RPPVersion + "|" + MerchantId + "|" + CustomerName + "|" + CustomerEmailorMobile + "|" + TotalAmount + "|" + NoteFromMerchant + "|" +
                    InvoiceNo + "|" + MerchantName + "|" + DistributorIdentifier + "|" + AgentMobileNumber + "|" + CustomerId + "|" + Product +
                    "||||||||||" +
                    MerchantEmailorMobile;
            System.IO.StreamWriter file1 = new StreamWriter(filePath, true);
            file1.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
            file1.WriteLine("\\ncbs Request :" + request);
            file1.WriteLine("\\ncbs DATE TIME :" + DateTime.Now.ToString());
            file1.Close();
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : Input - " + request);
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : GetInput - End");
            return request;
        }
        private string GetFilePath(string? logPath)
        {
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : GetFilePath - Start | LogPath - " + logPath);
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
            _logger.LogInformation("PaymentGatewayPaynimo - GetPaymentLinkDetails : GetFilePath - End | FilePath - " + filePath);
            return filePath;
        }
        
        #endregion Private
    }
}