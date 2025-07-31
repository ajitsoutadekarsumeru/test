using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTriggerByTriggerIdsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTriggerByTriggerIdsMapperConfiguration() : base()
        {
            CreateMap<CommunicationTrigger, GetTriggerDetailsByTriggerIdsDto>();
        }
    }
}
