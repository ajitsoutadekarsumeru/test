using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class DuplicateReceiptNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public string PaymentDates { get; set; } = string.Empty;
        public string InstrumentDate { get; set; } = string.Empty;
        public CollectionDtoWithId _dto { get; set; } = new CollectionDtoWithId();

        private readonly NotificationSettings _notificationSettings;

        public DuplicateReceiptNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionDtoWithId dto)
        {
            _dto = dto;
            var SMSSignature = _notificationSettings.SmsSignature;

            PaymentDates = Convert.ToDateTime(_dto.CollectionDate).ToString("dd/MM/yyyy");
            InstrumentDate = (_dto.Cheque != null && _dto.Cheque.InstrumentDate != null) ?
                                        Convert.ToDateTime(_dto.Cheque.InstrumentDate).ToString("dd/M/yyyy") : string.Empty;

            string receiptIssuanceDate = Convert.ToDateTime(_dto.CollectionDate).ToString("dd/MMM/yy");

            SMSMessage = "THIS IS A DUPLICATE RECEIPT, your payment of Rs. "
                + _dto.Amount + ", collected on " + receiptIssuanceDate + ", receipt no "
                + _dto.CustomId + ". Thanks " + SMSSignature + "";

            GetEmailTemplate();
        }

        private void GetEmailTemplate()
        {
            switch (_dto.CollectionMode.ToLower())
            {
                case "dd":
                    GetDDTemplate();
                    break;

                case "cash":
                    GetCashTemplate();
                    break;

                case "cheque":
                    GetChequeTemplate();
                    break;

                case "neft":
                    GetNEFTTemplate();
                    break;

                case "rtgs":
                    GetRTGSTemplate();
                    break;
            }
        }

        private void GetRTGSTemplate()
        {
            EmailSubject = "ENCollect: DUPLICATE Receipt for " + _dto.CollectionMode + " given by you";
            EmailMessage = "<p><b> THIS IS A DUPLICATE RECEIPT </b></p><br/>" + Environment.NewLine +
                           "<p>Receipt No." + _dto.CustomId + "</p><br/> " + Environment.NewLine +
                           "<p>Receipt issued date." + PaymentDates + "</p><br/> " + Environment.NewLine +
                           "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                           "<p> Thank you for the payment of EMI of Rs. " + _dto.Amount + " vide " + _dto.CollectionMode + " No." + _dto.Cheque.InstrumentNo +
                           " drawn on " + _dto.Cheque.BankName + " " + _dto.Cheque.BranchName + " dated " + InstrumentDate +
                           " towards your Loan Account No. " + _dto.Account.CustomId + ".<br/>" +
                           "<p>Please note that the Payment is subject to realization of " + _dto.CollectionMode + " </p><br/>" +
                           "<p><b>Collected By : </b>" + _dto.Collector.FirstName + " " + _dto.Collector.LastName + "<br/>" +
                           "<b>Emp Number : </b>" + _dto.Collector.CustomId + "<br/>" +
                           "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ".<br/>" +

                           "<br/>The contact details provided by you have been recorded, phone number " + _dto.MobileNo + ", email address " + _dto.EMailId + "." +
                           "</p><br/>" +
                           "<p>Thanks & regards <br/>" +
                            "ENCollect.<br/>" +
                            "<br/><br/>NO SIGNATURE IS REQUIRED AS THIS IS A COMPUTER GENERATE MAIL" +
                            "..........................................................................................................";
        }

        private void GetNEFTTemplate()
        {
            EmailSubject = "ENCollect: DUPLICATE Receipt for " + _dto.CollectionMode + " given by you";
            EmailMessage = "<p><b> THIS IS A DUPLICATE RECEIPT </b></p><br/>" + Environment.NewLine +
                          "<p>Receipt No." + _dto.CustomId + "</p><br/> " + Environment.NewLine +
                          "<p>Receipt issued date." + PaymentDates + "</p><br/> " + Environment.NewLine +
                          "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                          "<p> Thank you for the payment of EMI of Rs. " + _dto.Amount + " vide " + _dto.CollectionMode + " No." + _dto.Cheque.InstrumentNo +
                          " drawn on " + _dto.Cheque.BankName + " " + _dto.Cheque.BranchName + " dated " + InstrumentDate +
                          " towards your Loan Account No. " + _dto.Account.CustomId + ".<br/>" +
                          "<p>Please note that the Payment is subject to realization of " + _dto.CollectionMode + " </p><br/>" +
                          "<p><b>Collected By : </b>" + _dto.Collector.FirstName + " " + _dto.Collector.LastName + "<br/>" +
                          "<b>Emp Number : </b>" + _dto.Collector.CustomId + "<br/>" +
                          "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ".<br/>" +

                          "<br/>The contact details provided by you have been recorded, phone number " + _dto.MobileNo + ", email address " + _dto.EMailId + "." +
                          "</p><br/>" +
                          "<p>Thanks & regards <br/>" +
                           "ENCollect.<br/>" +
                           "<br/><br/>NO SIGNATURE IS REQUIRED AS THIS IS A COMPUTER GENERATE MAIL" +
                           "..........................................................................................................";
        }

        private void GetChequeTemplate()
        {
            EmailSubject = "ENCollect: DUPLICATE Receipt for " + _dto.CollectionMode + " given by you";
            EmailMessage = "<p><b> THIS IS A DUPLICATE RECEIPT </b></p><br/>" + Environment.NewLine +
                           "<p>Receipt No." + _dto.CustomId + "</p><br/> " + Environment.NewLine +
                           "<p>Receipt issued date." + PaymentDates + "</p><br/> " + Environment.NewLine +
                           "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                           "<p> Thank you for the payment of EMI of Rs. " + _dto.Amount + " vide " + _dto.CollectionMode + " No." + _dto.Cheque.InstrumentNo +
                           " drawn on " + _dto.Cheque.BankName + " " + _dto.Cheque.BranchName + " dated " + InstrumentDate +
                           " towards your Loan Account No. " + _dto.Account.CustomId + ".<br/>" +
                           "<p>Please note that the Payment is subject to realization of " + _dto.CollectionMode + " </p><br/>" +
                           "<p><b>Collected By : </b>" + _dto.Collector.FirstName + " " + _dto.Collector.LastName + "<br/>" +
                           "<b>Emp Number : </b>" + _dto.Collector.CustomId + "<br/>" +
                           "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ".<br/>" +

                           "<br/>The contact details provided by you have been recorded, phone number " + _dto.MobileNo + ", email address " + _dto.EMailId + "." +
                           "</p><br/>" +
                           "<p>Thanks & regards <br/>" +
                            "ENCollect.<br/>" +
                            "<br/><br/>NO SIGNATURE IS REQUIRED AS THIS IS A COMPUTER GENERATE MAIL" +
                            "..........................................................................................................";
        }

        private void GetCashTemplate()
        {
            EmailSubject = "ENCollect: DUPLICATE Receipt for " + _dto.CollectionMode + " given by you";

            EmailMessage = "<p><b> THIS IS A DUPLICATE RECEIPT </b></p><br/>" + Environment.NewLine +
                           "<p>Receipt No." + _dto.CustomId + "</p><br/> " + Environment.NewLine +
                           "<p>Receipt issued date." + PaymentDates + "</p><br/> " + Environment.NewLine +
                           "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                           "<p> Thank you for the payment of EMI of Rs. " + _dto.Amount + " vide " + _dto.CollectionMode + " No." + _dto.Cheque.InstrumentNo +
                           " drawn on " + _dto.Cheque.BankName + " " + _dto.Cheque.BranchName + " dated " + InstrumentDate +
                           " towards your Loan Account No. " + _dto.Account.CustomId + ".<br/>" +
                           "<p>Please note that the Payment is subject to realization of " + _dto.CollectionMode + " </p><br/>" +
                           "<p><b>Collected By : </b>" + _dto.Collector.FirstName + " " + _dto.Collector.LastName + "<br/>" +
                           "<b>Emp Number : </b>" + _dto.Collector.CustomId + "<br/>" +
                           "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ".<br/>" +

                           "<br/>The contact details provided by you have been recorded, phone number " + _dto.MobileNo + ", email address " + _dto.EMailId + "." +
                           "</p><br/>" +
                           "<p>Thanks & regards <br/>" +
                            "ENCollect.<br/>" +
                            "<br/><br/>NO SIGNATURE IS REQUIRED AS THIS IS A COMPUTER GENERATED MAIL" +
                            "..........................................................................................................";
        }

        private void GetDDTemplate()
        {
            EmailMessage =
                "<p><b> THIS IS A DUPLICATE RECEIPT </b></p><br/>" + Environment.NewLine +
                  "<p>Receipt No. " + _dto.CustomId + "</p><br/>" + Environment.NewLine +
                  "<p>Receipt issued date." + PaymentDates + "</p><br/> " + Environment.NewLine +
                  "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                  "<p>" +
                  "Thank you for the payment of EMI of Rs." + _dto.Amount + "  vide " + _dto.CollectionMode + " No." + _dto.Cheque.InstrumentNo +
                  " drawn on  " + _dto.Cheque.BankName + " " + _dto.Cheque.BranchName + " dated " + InstrumentDate
                  + " towards your Loan Account No. " + _dto.Account.CustomId + ". <br/>" +
                  "<p>Please note that the Payment is subject to realization of " + _dto.CollectionMode + " </p><br/>" +
                  "<p><b>Collected By: </b>" + _dto.Collector.FirstName + " " + _dto.Collector.LastName + "<br/>" +
                  "<b>Emp Number:</b> " + _dto.Collector.CustomId + ".<br/>" +
                  "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ". <br/>" +
                  "<br/>The contact details provided by you have been recorded, phone number "
                  + _dto.MobileNo + ", email address "
                  + _dto.EMailId + "." +
                  "</p><br/>" +
                "<p>Thanks & regards <br/>" +
                            "ENCollect.<br/>" +
                            "<br/><br/>NO SIGNATURE IS REQUIRED AS THIS IS A COMPUTER GENERATE MAIL<br/>" +
                            "..........................................................................................................";

            EmailSubject = "ENCollect: DUPLICATE Receipt for " + _dto.CollectionMode + " given by you";
        }
    }
}