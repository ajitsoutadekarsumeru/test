using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateTriggerStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateTriggerStatusMapperConfiguration() : base()
        {
            CreateMap<UpdateTriggerStatusDto, CommunicationTrigger>();

        }
    }
}
