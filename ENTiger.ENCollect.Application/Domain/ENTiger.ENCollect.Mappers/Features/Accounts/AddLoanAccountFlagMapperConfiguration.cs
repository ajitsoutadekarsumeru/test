using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddLoanAccountFlagMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddLoanAccountFlagMapperConfiguration() : base()
        {
            CreateMap<AddLoanAccountFlagDto, LoanAccountFlag>()
                .ForMember(o => o.LoanAccountId, opt => opt.MapFrom(o => o.AccountId));
        }
    }
}