using Sumeru.Flex;
using ENTiger.ENCollect.HierarchyModule.AddGeoMasterHierarchyPlugins;

namespace ENTiger.ENCollect.HierarchyModule
{
    public class AddGeoMasterSequence : FlexiBusinessRuleSequenceBase<AddGeoMasterDataPacket>
    {
        public AddGeoMasterSequence()
        {            
            this.Add<CheckForDuplicatesAtLevel>(); 
            this.Add<CheckIfParentIdExists>();
            this.Add<CheckIfLevelIdExists>();
        }
    }
}
