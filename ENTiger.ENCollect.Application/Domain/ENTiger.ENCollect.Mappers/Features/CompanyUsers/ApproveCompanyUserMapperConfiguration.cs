using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveCompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApproveCompanyUserMapperConfiguration() : base()
        {
            CreateMap<ApproveCompanyUserDto, CompanyUser>();
        }
    }
}