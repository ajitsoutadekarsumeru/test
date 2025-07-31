using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule;

public partial class UserBucketScopeMapperConfiguration : FlexMapperProfile
{
    public UserBucketScopeMapperConfiguration() : base()
    {
        CreateMap<UserBucketScopeDto, UserBucketScope>();
        CreateMap<UserBucketScope, UserBucketScopeDto>();
        CreateMap<UserBucketScopeDtoWithId, UserBucketScope>();
        CreateMap<UserBucketScope, UserBucketScopeDtoWithId>();
    }
}
