namespace ENTiger.ENCollect.CollectionsModule
{
    public class SendPaymentLinkCommand : FlexCommandBridge<SendPaymentLinkDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}