namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ExecuteTreatmentPostBusDataPacket : FlexiFlowDataPacketWithCommandBridge<ExecuteTreatmentCommand>
    {
        #region "Properties

        //Models and other properties goes here
        public Treatment outputModel { get; set; }

        public List<string> segments { get; set; }

        public DateTime? treatExecutionStartdate { get; set; }

        public DateTime? treatExecutionEnddate { get; set; }

        public string? TreatmentHistoryId { get; set; }

        #endregion "Properties
    }
}