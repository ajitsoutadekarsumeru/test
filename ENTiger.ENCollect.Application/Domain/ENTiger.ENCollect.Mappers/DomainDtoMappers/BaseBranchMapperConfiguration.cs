using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BaseBranchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BaseBranchMapperConfiguration() : base()
        {
            CreateMap<BaseBranchDto, BaseBranch>();
            CreateMap<BaseBranch, BaseBranchDto>();
            CreateMap<BaseBranchDtoWithId, BaseBranch>();
            CreateMap<BaseBranch, BaseBranchDtoWithId>();
        }
    }
}