using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyAccountsSummaryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMyAccountsSummaryMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetMyAccountsSummaryDto>();

        }
    }
}
