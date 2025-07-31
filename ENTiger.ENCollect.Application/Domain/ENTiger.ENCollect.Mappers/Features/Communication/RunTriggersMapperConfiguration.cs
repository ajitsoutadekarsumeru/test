using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RunTriggersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public RunTriggersMapperConfiguration() : base()
        {
            CreateMap<RunTriggersDto, CommunicationTrigger>();

        }
    }
}
