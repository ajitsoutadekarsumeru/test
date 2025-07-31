using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserMapperConfiguration() : base()
        {
            CreateMap<CompanyUserDto, CompanyUser>();
            CreateMap<CompanyUser, CompanyUserDto>();
            CreateMap<CompanyUserDtoWithId, CompanyUser>();
            CreateMap<CompanyUser, CompanyUserDtoWithId>();
        }
    }
}