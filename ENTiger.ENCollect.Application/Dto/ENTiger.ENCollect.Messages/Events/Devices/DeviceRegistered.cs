namespace ENTiger.ENCollect.DevicesModule
{
    public class DeviceRegistered : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}