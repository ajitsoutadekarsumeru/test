namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetKeyPostBusDataPacket : FlexiFlowDataPacketWithCommandBridge<GetKeyCommand>
    {
        #region "Properties

        //Models and other properties goes here
        public string ReferenceId { get; set; }

        public string Key { get; set; }

        #endregion "Properties
    }
}