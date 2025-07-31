using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectCompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RejectCompanyUserMapperConfiguration() : base()
        {
            CreateMap<RejectCompanyUserDto, CompanyUser>();
        }
    }
}