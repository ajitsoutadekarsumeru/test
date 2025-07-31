using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule;

public partial class UserProductScopeMapperConfiguration : FlexMapperProfile
{
    public UserProductScopeMapperConfiguration() : base()
    {
        CreateMap<UserProductScopeDto, UserProductScope>();
        CreateMap<UserProductScope, UserProductScopeDto>();
        CreateMap<UserProductScopeDtoWithId, UserProductScope>();
        CreateMap<UserProductScope, UserProductScopeDtoWithId>();
    }
}
