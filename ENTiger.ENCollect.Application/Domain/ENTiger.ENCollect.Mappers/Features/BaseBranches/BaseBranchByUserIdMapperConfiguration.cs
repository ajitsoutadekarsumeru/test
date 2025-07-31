using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BaseBranchByUserIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BaseBranchByUserIdMapperConfiguration() : base()
        {
            CreateMap<BaseBranch, BaseBranchByUserIdDto>();
        }
    }
}