namespace ENTiger.ENCollect.PayInSlipsModule
{
    public class CreateDepositSlipCommand : FlexCommandBridge<CreateDepositSlipDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}