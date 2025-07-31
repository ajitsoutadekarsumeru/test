namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(MakeDormantAgencyUserCommand cmd)
        {
            await _bus.Send(cmd);
        }
    }
}
