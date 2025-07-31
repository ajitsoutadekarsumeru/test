
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommandAsync(CustomerConsentResponseCommand cmd)
        {
            await _bus.Send(cmd);
        }

    }
}
