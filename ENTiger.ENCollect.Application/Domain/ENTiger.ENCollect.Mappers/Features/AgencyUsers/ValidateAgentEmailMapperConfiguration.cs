using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateAgentEmailMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ValidateAgentEmailMapperConfiguration() : base()
        {
            CreateMap<ValidateAgentEmailDto, AgencyUser>();
        }
    }
}