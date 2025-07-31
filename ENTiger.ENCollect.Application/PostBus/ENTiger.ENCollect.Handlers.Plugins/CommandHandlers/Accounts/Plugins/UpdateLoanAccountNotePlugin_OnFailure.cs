using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateLoanAccountNotePlugin : FlexiPluginBase, IFlexiPlugin<UpdateLoanAccountNotePostBusDataPacket>
    {
        private const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            NoteUpdationFailed @event = new NoteUpdationFailed
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}