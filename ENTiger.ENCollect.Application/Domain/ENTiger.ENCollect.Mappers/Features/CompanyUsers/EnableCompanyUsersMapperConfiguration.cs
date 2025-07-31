using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnableCompanyUsersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public EnableCompanyUsersMapperConfiguration() : base()
        {
            CreateMap<EnableCompanyUsersDto, CompanyUser>();

        }
    }
}
