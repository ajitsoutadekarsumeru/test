namespace ENTiger.ENCollect.TreatmentModule
{
    public class ExecuteFragmentedTreatmentDataPacket : FlexiFlowDataPacketWithCommandBridge<ExecuteFragmentedTreatmentCommand>
    {
        public Treatment outputModel { get; set; }
    }
}