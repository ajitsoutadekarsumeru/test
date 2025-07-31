using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ValidateAgentMapperConfiguration() : base()
        {
            CreateMap<ValidateAgentDto, AgencyUser>();
        }
    }
}