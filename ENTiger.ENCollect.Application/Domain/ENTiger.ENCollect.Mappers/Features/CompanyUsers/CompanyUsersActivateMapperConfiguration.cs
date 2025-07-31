using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CompanyUsersActivateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CompanyUsersActivateMapperConfiguration() : base()
        {
            CreateMap<CompanyUsersActivateDto, CompanyUser>();

        }
    }
}
