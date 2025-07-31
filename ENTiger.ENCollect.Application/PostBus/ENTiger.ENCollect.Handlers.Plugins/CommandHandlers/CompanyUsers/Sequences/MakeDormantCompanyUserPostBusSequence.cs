using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class MakeDormantCompanyUserPostBusSequence : FlexiPluginSequenceBase<MakeDormantCompanyUserPostBusDataPacket>
    {
        public MakeDormantCompanyUserPostBusSequence()
        {
            this.Add<MakeDormantCompanyUserPlugin>();
        }
    }
}
