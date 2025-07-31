namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdatePayuResponsePostBusDataPacket : FlexiFlowDataPacketWithCommandBridge<UpdatePayuResponseCommand>
    {
        #region "Properties

        public string? CollectionId { get; set; }
        //Models and other properties goes here

        #endregion "Properties
    }
}