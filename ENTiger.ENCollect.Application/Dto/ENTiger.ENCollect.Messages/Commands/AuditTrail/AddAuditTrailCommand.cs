
namespace ENTiger.ENCollect.AuditTrailModule
{
    public class AddAuditTrailCommand : FlexCommandBridge<AddAuditTrailDto, FlexAppContextBridge>
    {
        public AuditEventData Data { get; set; }
    }
}
