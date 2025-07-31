using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBankBranchByBankIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBankBranchByBankIdMapperConfiguration() : base()
        {
            CreateMap<BankBranch, GetBankBranchByBankIdDto>()
                .ForMember(o => o.BankBranchId, opt => opt.MapFrom(o => o.Id))
                .ForMember(o => o.BranchName, opt => opt.MapFrom(o => o.Name))
                .ForMember(o => o.BranchCode, opt => opt.MapFrom(o => o.Code));
        }
    }
}