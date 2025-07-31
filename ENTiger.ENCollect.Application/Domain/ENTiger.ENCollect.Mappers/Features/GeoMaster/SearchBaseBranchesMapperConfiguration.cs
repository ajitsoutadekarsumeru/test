using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
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
            CreateMap<GeoMaster, SearchBaseBranchesDto>()
                .ForMember(d => d.Id, s => s.MapFrom(s => s.BaseBranchId))
                .ForMember(d => d.Name, s => s.MapFrom(s => s.BaseBranch.FirstName))
                .ForMember(d => d.Code, s => s.MapFrom(s => s.BaseBranch.CustomId));
        }
    }
}