using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetListMapperConfiguration() : base()
        {
            CreateMap<UserSearchCriteria, USGetListDto>();
        }
    }
}