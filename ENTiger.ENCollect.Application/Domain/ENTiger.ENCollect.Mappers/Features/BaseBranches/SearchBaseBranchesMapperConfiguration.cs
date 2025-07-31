using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchBaseBranchesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchBaseBranchesMapperConfiguration() : base()
        {
            CreateMap<BaseBranch, SearchBaseBranchDto>()
                .ForMember(d => d.Name, s => s.MapFrom(s => s.FirstName))
                .ForMember(d => d.Code, s => s.MapFrom(s => s.CustomId));
        }
    }
}