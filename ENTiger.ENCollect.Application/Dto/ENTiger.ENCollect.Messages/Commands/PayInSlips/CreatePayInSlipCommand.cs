namespace ENTiger.ENCollect.PayInSlipsModule
{
    public class CreatePayInSlipCommand : FlexCommandBridge<CreatePayInSlipDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}