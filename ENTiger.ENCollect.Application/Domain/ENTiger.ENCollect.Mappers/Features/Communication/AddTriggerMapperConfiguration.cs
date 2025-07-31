using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddTriggerMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public AddTriggerMapperConfiguration() : base()
        {
            CreateMap<AddTriggerDto, CommunicationTrigger>();

        }
    }
}
