using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public class ExecuteFragmentedTreatmentPostBusFFSequence : FlexiPluginSequenceBase<ExecuteFragmentedTreatmentDataPacket>
    {
        public ExecuteFragmentedTreatmentPostBusFFSequence()
        {
            //this.Add<ExecuteTreatmentPlugin>();
            this.Add<ExecuteTreatmentAddTreatmentHistoryFFPlugin>();
            this.Add<ExecuteTreatmentPOSCriteriaAllocationFFPlugin>();
            this.Add<ExecuteTreatmentAccountCriteriaAllocationFFPlugin>();
            this.Add<ExecuteTreatmentRoundRobinFFPlugin>();
            this.Add<ExecuteTreatmentByRuleAllocationFFPlugin>();
            this.Add<ExecuteTreatmentOnUpdateTrailFFPlugin>();
            // this.Add<ExecuteTreatmentOnCommunicationFFPlugin>();
            this.Add<ExecuteTreatmentOnPerformanceBandFFPlugin>();
            this.Add<ExecuteAddTreatmentHistoryToElasticSearchFFPlugin>();
        }
    }
}