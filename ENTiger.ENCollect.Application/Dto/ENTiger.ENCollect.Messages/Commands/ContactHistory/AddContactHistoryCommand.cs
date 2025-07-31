using ENTiger.ENCollect.AccountContactHistoryModule;

namespace ENTiger.ENCollect.Messages.Commands.ContactHistory
{
    public class AddContactHistoryCommand : FlexCommandBridge<AddContactHistoryDto, FlexAppContextBridge>
    {
       public AccountContactHistoryEventData data { get; set; }
        public FlexAppContextBridge? flexAppContext{ get; set; }
    }
}
