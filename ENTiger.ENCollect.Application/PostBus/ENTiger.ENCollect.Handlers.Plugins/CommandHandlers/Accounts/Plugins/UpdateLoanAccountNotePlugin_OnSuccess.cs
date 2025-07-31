using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateLoanAccountNotePlugin : FlexiPluginBase, IFlexiPlugin<UpdateLoanAccountNotePostBusDataPacket>
    {
        private const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            NoteUpdated @event = new NoteUpdated
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}