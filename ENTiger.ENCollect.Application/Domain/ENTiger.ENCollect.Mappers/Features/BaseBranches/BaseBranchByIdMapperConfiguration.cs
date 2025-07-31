using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BaseBranchByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BaseBranchByIdMapperConfiguration() : base()
        {
            CreateMap<BaseBranch, BaseBranchByIdDto>()
                .ForMember(o => o.Name, opt => opt.MapFrom(o => o.FirstName));
        }
    }
}