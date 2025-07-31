using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RejectAgentMapperConfiguration() : base()
        {
            CreateMap<RejectAgentDto, AgencyUser>();
        }
    }
}