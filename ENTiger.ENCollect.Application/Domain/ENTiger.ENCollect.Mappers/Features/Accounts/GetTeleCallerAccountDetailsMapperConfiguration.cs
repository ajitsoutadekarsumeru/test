using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTeleCallerAccountDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTeleCallerAccountDetailsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetTeleCallerAccountDetailsDto>();
        }
    }
}