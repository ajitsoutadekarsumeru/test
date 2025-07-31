using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RoleAccountFetchConfigMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public RoleAccountFetchConfigMapperConfiguration() : base()
        {
            CreateMap<RoleAccountFetchConfigDto, AccountScopeConfiguration>();
            CreateMap<AccountScopeConfiguration, RoleAccountFetchConfigDto>();
            CreateMap<RoleAccountFetchConfigDtoWithId, AccountScopeConfiguration>();
            CreateMap<AccountScopeConfiguration, RoleAccountFetchConfigDtoWithId>();

        }
    }
}
