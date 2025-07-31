using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateAgentMobileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ValidateAgentMobileMapperConfiguration() : base()
        {
            CreateMap<ValidateAgentMobileDto, AgencyUser>();
        }
    }
}