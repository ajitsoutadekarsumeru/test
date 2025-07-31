using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule
{
    public class AddGeoMasterPostBusSequence : FlexiPluginSequenceBase<AddGeoMasterPostBusDataPacket>
    {
        public AddGeoMasterPostBusSequence()
        {
            this.Add<AddGeoMasterPlugin>(); 
        }
    }
}
