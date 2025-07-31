using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule;

public partial class UserGeoScopeMapperConfiguration : FlexMapperProfile
{
    public UserGeoScopeMapperConfiguration() : base()
    {
        CreateMap<UserGeoScopeDto, UserGeoScope>();
        CreateMap<UserGeoScope, UserGeoScopeDto>();
        CreateMap<UserGeoScopeDtoWithId, UserGeoScope>();
        CreateMap<UserGeoScope, UserGeoScopeDtoWithId>();
    }
}
