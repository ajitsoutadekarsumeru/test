using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetRoleBasedSearchConfigMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetRoleBasedSearchConfigMapperConfiguration() : base()
        {
            CreateMap<AccountScopeConfiguration, GetRoleBasedSearchConfigDto>()
            .ForMember(o => o.UserType, opt => opt.MapFrom(o => o.AccountabilityTypeId));

        }
    }
}
