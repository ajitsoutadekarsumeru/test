using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RenewAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RenewAgentMapperConfiguration() : base()
        {
            CreateMap<RenewAgentDto, AgencyUser>();
        }
    }
}