using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAccountByAccountNoMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAccountByAccountNoMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetAccountByAccountNoDto>();
        }
    }
}