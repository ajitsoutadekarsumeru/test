using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountNotePlugin : FlexiPluginBase, IFlexiPlugin<AddLoanAccountNotePostBusDataPacket>
    {
        private const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            NoteAdded @event = new NoteAdded
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}