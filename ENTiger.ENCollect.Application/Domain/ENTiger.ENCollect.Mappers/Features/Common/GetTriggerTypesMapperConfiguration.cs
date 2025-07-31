using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTriggerTypesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTriggerTypesMapperConfiguration() : base()
        {
            CreateMap<TriggerType, GetTriggerTypesDto>();

        }
    }
}
