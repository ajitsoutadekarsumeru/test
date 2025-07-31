namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class PrintIdCardCommand : FlexCommandBridge<PrintIdCardDto, FlexAppContextBridge>
    {
        public string IdCardNumber { get; set; }
    }
}