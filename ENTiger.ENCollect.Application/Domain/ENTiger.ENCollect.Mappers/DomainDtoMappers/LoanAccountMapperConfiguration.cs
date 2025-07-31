using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LoanAccountMapperConfiguration() : base()
        {
            CreateMap<LoanAccountDto, LoanAccount>();
            CreateMap<LoanAccount, LoanAccountDto>();
            CreateMap<LoanAccountDtoWithId, LoanAccount>();
            CreateMap<LoanAccount, LoanAccountDtoWithId>();
        }
    }
}