using Sumeru.Flex;
using System.Data;

namespace ENTiger.ENCollect
{
    public class AccountImportPartialNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, DataTable records)
        {
            EmailSubject = "Partially Processed : Bulk Account Import via File - " + TransactionId;
            EmailMessage = "<p>Dear User, <br/><br/>" +
                                "Import Accounts via API received and partially processed.<br/><br/>" +
                                "No Of Records          : " + records.Select().ToList().Count + "<br/>" +
                                "No Of Records Inserted : " + records.Select("IsError = false AND IsInsert = true").ToList().Count + "<br/>" +
                                "No Of Records Updated  : " + records.Select("IsError = false AND IsInsert = false").ToList().Count + "<br/>" +
                                "No Of Error Records    : " + records.Select("IsError = true").ToList().Count + "</p>";
        }
    }
}