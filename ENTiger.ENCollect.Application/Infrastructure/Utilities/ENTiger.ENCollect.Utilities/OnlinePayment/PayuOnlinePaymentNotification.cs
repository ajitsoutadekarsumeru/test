using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PayuOnlinePaymentNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        public string WAMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;
        public PayuOnlinePaymentNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionDtoWithId _dto, PaymentTransactionDtoWithId result)
        {
            var smsSignature = _notificationSettings.SmsSignature;

            if (result?.Status == "PaymentLink generated" && _dto != null && !string.IsNullOrEmpty(_dto.MobileNo))
            {
                var accountNumberSuffix = GetAccountNumberSuffix(_dto.Account.AGREEMENTID);

                // SMS Message
                SMSMessage = GenerateSmsMessage(_dto.CustomerName, _dto.Amount, accountNumberSuffix, result.PaymentGateway.ReturnURL, smsSignature);

                // Email Message
                EmailSubject = "Payu Online Payment";
                EmailMessage = GenerateEmailMessage(_dto.CustomerName, _dto.Amount, accountNumberSuffix, result.PaymentGateway.ReturnURL);

                // WhatsApp Message
                WAMessage = GenerateWaMessage(_dto.CustomerName, _dto.Amount, _dto.Account.CustomId, result.PaymentGateway.ReturnURL);
            }
            else
            {
                HandleFailedTransaction(_dto, result, smsSignature);
            }
        }

        private string GetAccountNumberSuffix(string accountId)
        {
            return accountId.Substring(accountId.Length - 4);
        }

        private string GenerateSmsMessage(string customerName, decimal? amount, string accountNumber, string paymentUrl, string signature)
        {
            return $"Dear {customerName.Replace(" ", "")}, You can now pay your amount of {amount} for your account no {accountNumber} using the link: {paymentUrl}. Thanks {signature}";
            //return $"Dear Religare Finvest Limited customer, we have received Rs. 1000 by online vide eReceipt Number 20000 towards your Loan A/C no. 50000.";
        }

        private string GenerateEmailMessage(string customerName, decimal? amount, string accountNumber, string paymentUrl)
        {
            return $"<p> Dear {customerName.Replace(" ", "")}<br/>" +
                   $"<p>You can now pay your amount of {amount} on your account ending with xxxx{accountNumber} " +
                   $"using the link below for online payment.</p><br/>" +
                   $"<p><a href=\"{paymentUrl}\">Click Here</a><br/></p>";
        }

        private string GenerateWaMessage(string customerName, decimal? amount, string customId, string paymentUrl)
        {
            return $"Dear {customerName}, You can now pay {amount} for your loan account number {customId} using the link: {paymentUrl}.";
        }

        private void HandleFailedTransaction(CollectionDtoWithId _dto, PaymentTransactionDtoWithId result, string smsSignature)
        {
            var error = JsonConvert.SerializeObject(result);
            SMSMessage = $"Dear {_dto.CustomerName.Replace(" ", "")}, Your transaction for INR {_dto.Amount} using {_dto.CollectionMode} has failed. Thanks {smsSignature}";
        }
    }
}