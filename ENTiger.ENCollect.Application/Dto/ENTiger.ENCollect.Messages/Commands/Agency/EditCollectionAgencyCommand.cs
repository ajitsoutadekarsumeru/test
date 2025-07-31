namespace ENTiger.ENCollect.AgencyModule
{
    public class EditCollectionAgencyCommand : FlexCommandBridge<EditCollectionAgencyDto, FlexAppContextBridge>
    {
        public EditCollectionAgencyDto InputModel { get; set; }
        public string PartyId { get; set; }
    }
}