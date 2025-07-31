using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateLoanAccountFlagMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateLoanAccountFlagMapperConfiguration() : base()
        {
            CreateMap<UpdateLoanAccountFlagDto, LoanAccountFlag>();
        }
    }
}