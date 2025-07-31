
namespace ENTiger.ENCollect.SettlementModule
{
    public class RequestSettlementCommand : FlexCommandBridge<RequestSettlementDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }

    }
}
