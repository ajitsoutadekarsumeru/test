using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankBranchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BankBranchMapperConfiguration() : base()
        {
            CreateMap<BankBranchDto, BankBranch>();
            CreateMap<BankBranch, BankBranchDto>();
            CreateMap<BankBranchDtoWithId, BankBranch>();
            CreateMap<BankBranch, BankBranchDtoWithId>();
        }
    }
}