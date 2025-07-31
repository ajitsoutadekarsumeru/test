using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class MakeDormantAgencyUserPostBusSequence : FlexiPluginSequenceBase<MakeDormantAgencyUserPostBusDataPacket>
    {
        public MakeDormantAgencyUserPostBusSequence()
        {
            this.Add<MakeDormantAgencyUserPlugin>();
        }
    }
}
