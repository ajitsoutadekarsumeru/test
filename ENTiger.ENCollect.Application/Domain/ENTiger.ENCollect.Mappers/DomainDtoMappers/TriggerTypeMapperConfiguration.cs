using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TriggerTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTypeMapperConfiguration() : base()
        {
            CreateMap<TriggerTypeDto, TriggerType>();
            CreateMap<TriggerType, TriggerTypeDto>();
            CreateMap<TriggerTypeDtoWithId, TriggerType>();
            CreateMap<TriggerType, TriggerTypeDtoWithId>();

        }
    }
}
