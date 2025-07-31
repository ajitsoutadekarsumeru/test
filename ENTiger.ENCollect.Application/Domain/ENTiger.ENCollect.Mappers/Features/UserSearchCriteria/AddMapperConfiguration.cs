using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddMapperConfiguration() : base()
        {
            CreateMap<AddDto, UserSearchCriteria>()
            .ForMember(cm => cm.filterName, Dm => Dm.MapFrom(dModel => dModel.FilterName))
            ;
        }
    }
}