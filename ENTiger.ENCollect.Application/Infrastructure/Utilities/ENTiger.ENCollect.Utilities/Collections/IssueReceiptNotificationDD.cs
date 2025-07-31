using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class IssueReceiptNotificationDD : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CollectionDtoWithId _dto)
        {
            var InstrumentDate = (_dto.Cheque != null && _dto.Cheque.InstrumentDate != null) ?
                                        Convert.ToDateTime(_dto.Cheque.InstrumentDate).ToString("dd/M/yyyy") : string.Empty;

            try
            {
                EmailMessage =
                  "<p>Receipt No. " + _dto.CustomId + "</p><br/>" +
                  "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                  "<p>" +
                  "Thank you for the payment of EMI of Rs." + _dto.Amount + "  vide " + _dto.CollectionMode + " No." + _dto.Cheque.InstrumentNo +
                  " drawn on  " + _dto.Cheque.BankName + " " + _dto.Cheque.BranchName + " dated " + InstrumentDate
                  + " towards your Loan Account No. " + _dto.Account.CustomId + ". <br/>" +
                  "<p>Please note that the Payment is subject to realization of " + _dto.CollectionMode + " </p><br/>" +
                  "<p><b>Collected By: </b>" + _dto.Collector.FirstName + " " + _dto.Collector.LastName + "<br/>" +
                  "<b>Emp Number:</b> " + _dto.Collector.CustomId + ".<br/>" +
                  "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ". <br/>" +
                  "The contact details provided by you during money collection have been recorded, phone number " + _dto.MobileNo + ", email address "
                  + _dto.EMailId + "." +
                  "</p><br/>";
            }
            catch (Exception ex)
            {
                //Log error
            }
            EmailSubject = "ENCollect: Receipt for " + _dto.CollectionMode + " given by you";
        }
    }
}