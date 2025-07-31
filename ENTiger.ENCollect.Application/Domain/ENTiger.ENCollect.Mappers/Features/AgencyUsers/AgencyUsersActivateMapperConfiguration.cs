using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgencyUsersActivateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public AgencyUsersActivateMapperConfiguration() : base()
        {
            CreateMap<AgencyUsersActivateDto, AgencyUser>();

        }
    }
}
