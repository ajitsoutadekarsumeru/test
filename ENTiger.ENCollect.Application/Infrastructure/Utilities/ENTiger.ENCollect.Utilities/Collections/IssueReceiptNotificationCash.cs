using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class IssueReceiptNotificationCash : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CollectionDtoWithId _dto)
        {
            try
            {
                EmailMessage = " <p>Receipt No. " + _dto.CustomId + "</p><br/>" +
                        "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                        "<p>" +
                        "Thank you for the payment of Rs. " + _dto.Amount + " vide " + _dto.CollectionMode + " towards your " +
                        "Loan Account No." + _dto.Account.CustomId + ".<br/>" +
                        "<b>Receiving Agent Name:</b> " + _dto.Collector.FirstName + " " + _dto.Collector.LastName + " <br/>" +
                        "<b>Pan Number (if provided):</b>  " + _dto.yPANNo + ". <br/>" +
                        "The contact details provided by you during money collection have been recorded, phone number " + _dto.MobileNo + ", email address " + _dto.EMailId + ". " +
                        "</p><br/>";
            }
            catch (Exception ex)
            {
                //TODO
                //Log error
            }
            EmailSubject = "ENCollect: Receipt for " + _dto.CollectionMode + " given by you";
        }
    }
}