using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountNotePlugin : FlexiPluginBase, IFlexiPlugin<AddLoanAccountNotePostBusDataPacket>
    {
        private const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            NoteAdditionFailed @event = new NoteAdditionFailed
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}