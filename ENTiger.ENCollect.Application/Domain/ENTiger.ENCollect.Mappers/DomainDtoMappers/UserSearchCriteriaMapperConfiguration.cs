using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserSearchCriteriaMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserSearchCriteriaMapperConfiguration() : base()
        {
            CreateMap<UserSearchCriteriaDto, UserSearchCriteria>();
            CreateMap<UserSearchCriteria, UserSearchCriteriaDto>();
            CreateMap<UserSearchCriteriaDtoWithId, UserSearchCriteria>();
            CreateMap<UserSearchCriteria, UserSearchCriteriaDtoWithId>();
        }
    }
}