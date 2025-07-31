namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUploadPostBusDataPacket : FlexiFlowDataPacketWithCommandBridge<AgencyUploadCommand>
    {
        #region "Properties

        //Models and other properties goes here
        public string Id { get; set; }

        #endregion "Properties
    }
}