using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCreditCardAccountDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCreditCardAccountDetailsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetCreditCardAccountDetailsDto>();
        }
    }
}