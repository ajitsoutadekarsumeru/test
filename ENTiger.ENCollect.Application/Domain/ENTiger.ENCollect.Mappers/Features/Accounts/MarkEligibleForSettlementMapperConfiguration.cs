using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MarkEligibleForSettlementMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public MarkEligibleForSettlementMapperConfiguration() : base()
        {
            CreateMap<MarkEligibleForSettlementDto, LoanAccount>();

        }
    }
}
