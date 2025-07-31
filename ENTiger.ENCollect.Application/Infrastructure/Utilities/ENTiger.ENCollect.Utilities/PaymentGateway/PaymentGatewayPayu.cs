using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayPayu : IPaymentGateway
    {
        protected readonly ILogger<PaymentGatewayPayu> _logger;
        protected string _tenantId = string.Empty;
        protected string? url = string.Empty;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly IApiHelper _apiHelper;

        public PaymentGatewayPayu(ILogger<PaymentGatewayPayu> logger, 
            ISmsUtility smsUtility, 
            IEmailUtility emailUtility, 
            MessageTemplateFactory messageTemplateFactory,
            IApiHelper apiHelper)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<PaymentTransactionDto> GetPaymentLinkDetails(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails, string tenantId)
        {
            _logger.LogInformation("PaymentGatewayPayu : GetPaymentLinkDetails - Start");

            // Extract configuration values
            var configValues = paymentDetails.ToDictionary(x => x.Parameter.ToLowerInvariant(), x => x.Value);
            var url = configValues.GetValueOrDefault("payupaymenturl") ?? "";
            var payumerchantid = configValues.GetValueOrDefault("payumerchantid") ?? "";
            var logPath = configValues.GetValueOrDefault("payulogfilepath") ?? "";
            var filePath = GetFilePath(logPath);

            PaymentTransactionDto transaction = null;
            string authorizationToken;

            try
            {
                authorizationToken = await GetTokenAsync(paymentDetails);
                if (string.IsNullOrEmpty(authorizationToken))
                {
                    _logger.LogError("Authorization token is null or empty.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve authorization token: {ex.Message}");
                return null;
            }

            var input = GetPayuInput(collection, paymentDetails);
            string responseString = string.Empty;

            try
            {
                // Log request data
                await LogToFileAsync(filePath, new[]
                {
                   "<---------------------Start--------------------->",
                   "Plugin Name     : PaymentGatewayPayu OnlinePaymentNotificationService",
                   $"Url             : {url}",
                   $"Authorization   : {authorizationToken}",
                   $"Request Data    : {JsonConvert.SerializeObject(input)}",
                   $"Request DATETIME: {DateTime.Now}",
                   "***************************************************"
                });

                _logger.LogInformation(" PaymentGatewayPayu OnlinePaymentNotificationService : API Initiated");
                string jsonPayload = JsonConvert.SerializeObject(input);
               
                var headers = new Dictionary<string, string>
                {
                    { "Bearer", authorizationToken }, 
                    { "merchantId", payumerchantid } 
                };

                var response = await _apiHelper.SendRequestAsync(jsonPayload, url, HttpMethod.Post, headers);

                if (!response.IsSuccessStatusCode)
                {
                    await LogErrorDetailsAsync(filePath, response.StatusCode, response.ReasonPhrase, responseString);
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseString);

                    await LogSuccessDetailsAsync(filePath, response.StatusCode, result);
                    transaction = HandleSuccessResponse(responseString, url);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in API call: {ex.Message}");
                await LogExceptionAsync(filePath, ex);
            }

            _logger.LogInformation("OnlinePaymentNotificationService : GetPayuPaymentLinkDetails - End");
            return transaction;
        }

        private PaymentTransactionDto HandleSuccessResponse(string responseString, string url)
        {
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);
            if (result?.message == "PaymentLink generated")
            {
                return new PaymentTransactionDto
                {
                    MerchantTransactionId = result.result.invoiceNumber,
                    MerchantReferenceNumber = result.result.description,
                    ResponseMessage = JsonConvert.SerializeObject(result),
                    Status = result.message,
                    StatusCode = result.status,
                    Amount = result.result.totalAmount,
                    Currency = result.result.currency,
                    PaymentGateway = new PaymentGatewayDto
                    {
                        MerchantId = result.guid,
                        PostURL = url,
                        ReturnURL = result.result.paymentLink
                    }
                };
            }
            return null;
        }

        private async Task LogErrorDetailsAsync(string filePath, HttpStatusCode statusCode, string reason, string responseString)
        {
            await LogToFileAsync(filePath, new[]
            {
               $"Failed StatusCode   : {statusCode}",
               $"Error               : {responseString}",
               $"ReasonPhrase        : {reason}",
               $"Response DATETIME   : {DateTime.Now}",
               "<---------------------End--------------------->"
            });
        }

        private async Task LogSuccessDetailsAsync(string filePath, HttpStatusCode statusCode, dynamic result)
        {
            await LogToFileAsync(filePath, new[]
            {
              $"Success StatusCode  : {statusCode}",
              $"Response            : {JsonConvert.SerializeObject(result)}",
              $"Response DATETIME   : {DateTime.Now}",
              "<---------------------End--------------------->"
            });
        }

        private async Task LogExceptionAsync(string filePath, Exception ex)
        {
            await LogToFileAsync(filePath, new[]
            {
                 $"Exception           : {ex.Message}",
                 $"StackTrace          : {ex.StackTrace}",
                 $"Response DATETIME   : {DateTime.Now}",
                 "<---------------------End--------------------->"
             });
        }

        private async Task LogToFileAsync(string filePath, IEnumerable<string> lines)
        {
            await using var file = new StreamWriter(filePath, true);
            foreach (var line in lines)
            {
                await file.WriteLineAsync(line);
            }
        }

        private async Task<string> GetTokenAsync(List<FeatureMasterDtoWithId> paymentDetails)
        {
            var configValues = paymentDetails.ToDictionary(x => x.Parameter.ToLowerInvariant(), x => x.Value);
            var payuClientId = configValues.GetValueOrDefault("payuclientid") ?? string.Empty;
            var payuClientSecret = configValues.GetValueOrDefault("payuclientsecret") ?? string.Empty;
            var payuTokenUrl = configValues.GetValueOrDefault("payutokenurl") ?? string.Empty;
            var payugranttype = configValues.GetValueOrDefault("payugranttype") ?? "client_credentials";
            var payuscope = configValues.GetValueOrDefault("payuscope") ?? "create_payment_links";
            var logPath = configValues.GetValueOrDefault("payulogfilepath") ?? "";

            IEnumerable<KeyValuePair<string, string>> requestContent = new[]
            {
                new KeyValuePair<string, string>("grant_type", payugranttype),
                new KeyValuePair<string, string>("client_id", payuClientId),
                new KeyValuePair<string, string>("client_secret", payuClientSecret),
                new KeyValuePair<string, string>("scope", payuscope)
            };

            string logFilePath = GetFilePath(logPath);
            try
            {
                await LogToFileAsync(logFilePath, new[]
                {
                     "<---------------------Start--------------------->",
                     "API Name         : GetTokenAsync",
                     $"Request URL     : {payuTokenUrl}",
                     $"Request Body    : {string.Join(", ", requestContent.Select(kv => $"{kv.Key}: {kv.Value}"))}",
                     $"Request DATETIME: {DateTime.Now}",
                     "***************************************************"
                });

                _logger.LogInformation("Sending request to get token");

                var response = await _apiHelper.SendFormUrlEncodedRequestAsync(requestContent, payuTokenUrl, HttpMethod.Post);
                var responseBody = await response.Content.ReadAsStringAsync();

                // Log response details to file
                await LogToFileAsync(logFilePath, new[]
                {
                      $"Response Status Code: {response.StatusCode}",
                      $"Response Body       : {responseBody}",
                      $"Response DATETIME   : {DateTime.Now}",
                      "<---------------------End--------------------->"
                });

                response.EnsureSuccessStatusCode(); // Throws if not successful

                var tokenResponse = System.Text.Json.JsonDocument.Parse(responseBody);
                var accessToken = tokenResponse.RootElement.GetProperty("access_token").GetString();

                _logger.LogInformation("Token successfully retrieved");
                return accessToken;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException in retrieving token: {ex.Message}");
                await LogToFileAsync(logFilePath, new[]
                {
                  $"Exception           : HttpRequestException",
                  $"Message             : {ex.Message}",
                  $"StackTrace          : {ex.StackTrace}",
                  $"Response DATETIME   : {DateTime.Now}",
                  "<---------------------End--------------------->"
                });
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in retrieving token: {ex.Message}");
                await LogToFileAsync(logFilePath, new[]
                {
                    $"Exception           : {ex.GetType().Name}",
                    $"Message             : {ex.Message}",
                    $"StackTrace          : {ex.StackTrace}",
                    $"Response DATETIME   : {DateTime.Now}",
                    "<---------------------End--------------------->"
                });
                return null;
            }
        }

        public async Task SendPaymentLinkEmail(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId result, string tenantId)
        {
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.PayuOnlinePaymentTemplate(collectionDto, result, tenantId);

            //Send Email
            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
        }

        public async Task SendPaymentLinkSMS(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId result, string tenantId)
        {
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.PayuOnlinePaymentTemplate(collectionDto, result, tenantId);

            //Send SMS
            await _smsUtility.SendSMS(collectionDto.MobileNo, messageTemplate.SMSMessage, tenantId);
        }

        #region Private

        private T GetParameterValue<T>(List<FeatureMasterDtoWithId> paymentDetails, string parameterName, T defaultValue)
        {
            var value = paymentDetails
                .Where(x => x.Parameter.StartsWith(parameterName))
                .Select(x => x.Value)
                .FirstOrDefault();

            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        private object GetPayuInput(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails)
        {
            _logger.LogInformation("OnlinePaymentNotificationService : GetPayuInput - Start");

            // Retrieve values using the helper function
            bool sms = GetParameterValue(paymentDetails, PaymentGatewayEnum.PayUSMS.Value, false);
            bool email = GetParameterValue(paymentDetails, PaymentGatewayEnum.PayUEmail.Value, false);
            double timeInMins = GetParameterValue(paymentDetails, PaymentGatewayEnum.PayUTimeInMins.Value, 1440.0);
            var payuscope = GetParameterValue(paymentDetails, PaymentGatewayEnum.PayUSource.Value, "payment_link_onedash");

            // Calculate expiration time
            DateTime currentDateTime = DateTime.Now.AddMinutes(timeInMins);
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // Construct the request object
            var request = new
            {
                subAmount = collection.Amount,
                expiry = formattedDateTime,
                description = collection.CustomId,
                source = payuscope,
                customer = new
                {
                    name = collection.CustomerName,
                    phone = collection.MobileNo,
                    email = collection.EMailId
                },
                viaEmail = email,
                viaSms = sms
            };

            _logger.LogInformation("OnlinePaymentNotificationService : GetPayuInput - End | JSON - " + JsonConvert.SerializeObject(request));
            return request;
        }


        private string GetFilePath(string? logPath)
        {
            _logger.LogInformation("PaymentGatewayPayu - GetPaymentLinkDetails : GetFilePath - Start | LogPath - " + logPath);
            string filePath;
            string logname = "PayuPaymentGatewayLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
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
            _logger.LogInformation("PaymentGatewayPayu - GetPaymentLinkDetails : GetFilePath - End | FilePath - " + filePath);
            return filePath;
        }

        #endregion Private
    }
}