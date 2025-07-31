using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SecondaryUnAllocationFailedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        

        public virtual void ConstructData(string TransactionId,string unAllocationType)
        {
            EmailSubject = "Bulk Secondary UnAllocation Upload Rejected - " + TransactionId;
            if (unAllocationType?.ToLower() == "customerid level")
            {

                EmailMessage = "Dear User, <br><br>" +
                            "Your secondary unallocation file having TransactionId - "
                            + TransactionId + " has been rejected due to incorrect headers in the file or the file format being incorrect.<br><br>" +
                            "Note : Correct headers should be : CustomerId without any double quotes or spaces.<br>";
            }
            else
            {
                EmailMessage = "Dear User, <br><br>" +
                           "Your secondary unallocation file having TransactionId - "
                           + TransactionId + " has been rejected due to incorrect headers in the file or the file format being incorrect.<br><br>" +
                           "Note : Correct headers should be : AccountNo without any double quotes or spaces.<br>";
            }

        }
    }
}