using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeactivateCompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeactivateCompanyUserMapperConfiguration() : base()
        {
            CreateMap<DeactivateCompanyUserDto, CompanyUser>();
        }
    }
}