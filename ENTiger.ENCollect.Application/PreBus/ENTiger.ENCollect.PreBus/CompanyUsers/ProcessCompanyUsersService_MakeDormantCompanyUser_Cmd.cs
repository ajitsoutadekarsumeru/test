namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(MakeDormantCompanyUserCommand cmd)
        {
            await _bus.Send(cmd);
        }
    }
}