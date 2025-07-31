using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetAccountByAccountNumberDto>();
        }
    }
}