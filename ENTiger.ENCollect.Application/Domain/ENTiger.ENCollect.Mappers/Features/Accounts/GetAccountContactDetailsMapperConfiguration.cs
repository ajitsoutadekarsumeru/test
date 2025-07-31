using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAccountContactDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAccountContactDetailsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetAccountContactDetailsDto>();
        }
    }
}