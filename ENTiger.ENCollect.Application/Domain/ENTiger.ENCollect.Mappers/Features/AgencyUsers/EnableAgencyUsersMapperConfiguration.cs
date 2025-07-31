using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnableAgencyUsersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public EnableAgencyUsersMapperConfiguration() : base()
        {
            CreateMap<EnableAgencyUsersDto, AgencyUser>();

        }
    }
}
