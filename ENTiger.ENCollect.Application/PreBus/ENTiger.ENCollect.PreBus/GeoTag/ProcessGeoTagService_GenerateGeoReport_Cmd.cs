namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(GenerateGeoReportCommand cmd)
        {
            await _bus.Send(cmd);

        }

    }
}
