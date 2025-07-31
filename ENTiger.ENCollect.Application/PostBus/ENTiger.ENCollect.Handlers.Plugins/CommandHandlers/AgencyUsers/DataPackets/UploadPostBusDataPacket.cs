namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UploadPostBusDataPacket : FlexiFlowDataPacketWithCommandBridge<UploadCommand>
    {
        #region "Properties

        //Models and other properties goes here
        public string Id { get; set; }

        #endregion "Properties

    }
}