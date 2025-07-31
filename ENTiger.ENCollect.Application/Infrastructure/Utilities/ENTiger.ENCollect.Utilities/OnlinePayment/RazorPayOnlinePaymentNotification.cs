using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class RazorPayOnlinePaymentNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public string WAMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public RazorPayOnlinePaymentNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionDtoWithId _dto, PaymentTransactionDtoWithId Result)
        {
            var SMSSignature = _notificationSettings.SmsSignature;

            if (Result != null && Result.Status == "created")
            {
                if (!string.IsNullOrEmpty(_dto.MobileNo))
                {
                    string AccountNumber = _dto.Account.AGREEMENTID.Substring(_dto.Account.AGREEMENTID.Length - 4);

                    SMSMessage = "Dear " + _dto.CustomerName.Replace(" ", "") + ", You can now pay your amount of " + _dto.Amount + " for your account no " + AccountNumber + " using the below link for online payment: " + Result.PaymentGateway.ReturnURL + " Thanks " + SMSSignature + "";
                    //SmsUtility.SendSMS(_dto.MobileNo, message, _tenantId);

                    EmailSubject = "Pay your My Bank Online";
                    EmailMessage = "<p> Dear " + _dto.CustomerName.Replace(" ", "") + "<br/>" +
                                            "<p>You can now pay your amount of " + _dto.Amount + " on your account ending with xxxx" + AccountNumber +
                                            " ,using the below link for online payment.</p><br/>" +
                                            "<p><a href=" + Result.PaymentGateway.ReturnURL + ">Click Here</a><br/></p>";

                    //EmailUtility.SendMail(_dto.EMailId, mailmessage, subject, _tenantId);

                    WAMessage = "Dear " + _dto.CustomerName + ", You can now pay amount of " + _dto.Amount + " for your loan account number " + _dto.Account.CustomId + " using the below link for online payment. " + Result.PaymentGateway.ReturnURL + " .";
                    //TODO
                    //WhatsAppIntegrationUtilityService.SendWA_message(wamsg, _dto.MobileNo, _tenantId, "3446175");
                }
            }
            else
            {
                string error = JsonConvert.SerializeObject(Result);
                SMSMessage = "Dear " + _dto.CustomerName.Replace(" ", "") + ", Your transaction for INR " + _dto.Amount + " using " + _dto.CollectionMode + " has failed. Thanks " + SMSSignature + "";
                //Send SMS
                //SmsUtility.SendSMS(_dto.MobileNo, message, _tenantId);

                //Updatecollections("Error occured while generating payment link");
            }
        }
    }
}