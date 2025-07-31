using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommunicationTriggerMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CommunicationTriggerMapperConfiguration() : base()
        {
            CreateMap<CommunicationTriggerDto, CommunicationTrigger>();
            CreateMap<CommunicationTrigger, CommunicationTriggerDto>();
            CreateMap<CommunicationTriggerDtoWithId, CommunicationTrigger>();
            CreateMap<CommunicationTrigger, CommunicationTriggerDtoWithId>();

        }
    }
}
