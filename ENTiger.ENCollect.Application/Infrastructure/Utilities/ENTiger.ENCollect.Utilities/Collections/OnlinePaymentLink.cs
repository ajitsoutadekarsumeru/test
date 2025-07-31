using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class OnlinePaymentLink : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;

        public OnlinePaymentLink(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionDtoWithId _dto, string Response)
        {
            var values = Response.Split('|');
            var SMSSignature = _notificationSettings.SmsSignature;

            if (!string.IsNullOrEmpty(_dto.MobileNo) && string.Equals(values[25], "success", StringComparison.OrdinalIgnoreCase))
            {
                if (_dto.Account != null)
                {
                    string AccountNumber = _dto.Account.CustomId.Substring(_dto.Account.CustomId.Length - 4);
                    SMSMessage = "Dear " + _dto.CustomerName.Replace(" ", "") + ", You can now pay your amount of "
                        + _dto.Amount + " for your account no " + AccountNumber + " using the below link for online payment: "
                        + values[23] + ". Thanks " + SMSSignature + "";

                    //SmsUtility.SendSMS(_dto.MobileNo, message, _tenantId);

                    EmailSubject = "Pay your My Bank Online";
                    EmailMessage = "<p> Dear " + _dto.CustomerName.Replace(" ", "") + "<br/>" +
                                    "<p>You can now pay your amount of " + _dto.Amount + " on your account ending with xxxx" + AccountNumber + " ,using the below link for online payment.</p><br/>" +
                                 //"<p>You can now pay your amount of " + _dto.Amount + " using the below link for online payment.</p><br/>" +
                                 "<p><a href=" + values[23] + ">Click Here</a><br/></p>" +
                                 "NO SIGNATURE IS REQUIRED AS THIS IS A COMPUTER GENERATE MAIL";

                    //EmailUtility.SendMail(_dto.EMailId, mailmessage, subject, _tenantId);
                }
                else
                {
                    var message = "Dear " + _dto.CustomerName.Replace(" ", "") + " ,You can now pay your overdue amount of " + _dto.Amount + " for your account number XXXX using the below link for online payment." + values[23] + " Regards, My Bank";

                    EmailSubject = "Pay your My Bank Online";
                    EmailMessage = "<p>Dear " + _dto.CustomerName.Replace(" ", "") + ",</p><br/><p> You can now pay your overdue amount of  " + _dto.Amount + " for your account number XXXX using the below link for online payment.</p><br/><a href=" + values[23] + "> Click Here</a>";
                    //EmailUtility.SendMail(_dto.EMailId, msg, subject, _tenantId);
                }
            }
            else
            {
                SMSMessage = "Dear " + _dto.CustomerName.Replace(" ", "") + ", Your transaction for INR " + _dto.Amount + " using " + _dto.CollectionMode + " has failed. Thanks " + SMSSignature + "";

                //Send SMS
                //SmsUtility.SendSMS(_dto.MobileNo, message, _tenantId);
                //Updatecollections(values[25]);
            }
        }
    }
}