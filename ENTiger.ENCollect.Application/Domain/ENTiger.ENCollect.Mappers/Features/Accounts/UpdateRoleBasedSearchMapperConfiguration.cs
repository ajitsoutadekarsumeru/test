using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateRoleBasedSearchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateRoleBasedSearchMapperConfiguration() : base()
        {
            CreateMap<UpdateAccountScopeConfigurationDto, AccountScopeConfiguration>();
            CreateMap<AccountScopeConfigurationDto, AccountScopeConfiguration>()
            .ForMember(o => o.AccountabilityTypeId, opt => opt.MapFrom(o => o.UserType));
            

        }
    }
}
