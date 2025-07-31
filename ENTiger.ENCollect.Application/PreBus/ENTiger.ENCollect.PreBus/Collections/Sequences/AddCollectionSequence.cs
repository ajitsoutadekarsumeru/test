using Sumeru.Flex;
using ENTiger.ENCollect.CollectionsModule.AddCollectionCollectionsPlugins;

namespace ENTiger.ENCollect.CollectionsModule
{
    public class AddCollectionSequence : FlexiBusinessRuleSequenceBase<AddCollectionDataPacket>
    {
        public AddCollectionSequence()
        {

            this.Add<CheckUserCollectionLimit>(); this.Add<ValidateCollection>(); this.Add<CheckDuplicateReceipt>(); this.Add<FundsAvailabilityPlugin>();
        }
    }
}
