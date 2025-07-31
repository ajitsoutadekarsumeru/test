namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class ProcessHierarchyService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(AddGeoMasterCommand cmd)
        {
            await _bus.Send(cmd);
        }
    }
}
