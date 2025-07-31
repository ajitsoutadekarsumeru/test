using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCompanyUserMapperConfiguration() : base()
        {
            CreateMap<CompanyUser, SearchCompanyUserDto>()
                .ForMember(o => o.SinglePointReportingManagerFirstName, opt => opt.MapFrom(o => o.SinglePointReportingManager.FirstName))
                .ForMember(o => o.SinglePointReportingManagerLastName, opt => opt.MapFrom(o => o.SinglePointReportingManager.LastName))
                .ForMember(o => o.BaseBranchFirstName, opt => opt.MapFrom(o => o.BaseBranch.FirstName))
                .ForMember(o => o.BaseBranchLastName, opt => opt.MapFrom(o => o.BaseBranch.LastName))
                .ForMember(o => o.ENCollectId, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.status, opt => opt.MapFrom(o => o.CompanyUserWorkflowState.Name.Replace("CompanyUser", "")))
                .ForMember(o => o.WalletLimit, opt => opt.MapFrom(o => o.Wallet != null ? o.Wallet.WalletLimit : 0));
        }
    }
}