using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeactivateAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeactivateAgentMapperConfiguration() : base()
        {
            CreateMap<DeactivateAgentDto, AgencyUser>();
        }
    }
}