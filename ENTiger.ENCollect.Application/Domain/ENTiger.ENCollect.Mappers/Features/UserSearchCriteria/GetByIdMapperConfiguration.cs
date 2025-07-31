using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetByIdMapperConfiguration() : base()
        {
            CreateMap<UserSearchCriteria, GetByIdDto>();
        }
    }
}