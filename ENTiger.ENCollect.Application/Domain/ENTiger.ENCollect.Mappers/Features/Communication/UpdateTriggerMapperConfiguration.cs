using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateTriggerMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateTriggerMapperConfiguration() : base()
        {
            CreateMap<UpdateTriggerDto, CommunicationTrigger>();
           

        }
    }
}
