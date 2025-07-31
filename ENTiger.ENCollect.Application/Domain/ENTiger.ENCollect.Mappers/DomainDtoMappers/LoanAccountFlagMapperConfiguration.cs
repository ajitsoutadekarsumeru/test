using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountFlagMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LoanAccountFlagMapperConfiguration() : base()
        {
            CreateMap<LoanAccountFlagDto, LoanAccountFlag>();
            CreateMap<LoanAccountFlag, LoanAccountFlagDto>();
            CreateMap<LoanAccountFlagDtoWithId, LoanAccountFlag>();
            CreateMap<LoanAccountFlag, LoanAccountFlagDtoWithId>();
        }
    }
}