using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BaseBranchListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BaseBranchListMapperConfiguration() : base()
        {
            CreateMap<BaseBranch, BaseBranchListDto>()
                .ForMember(o => o.Name, opt => opt.MapFrom(o => o.FirstName))
                .ForMember(o => o.Code, opt => opt.MapFrom(o => o.CustomId));
        }
    }
}