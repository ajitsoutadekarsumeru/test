using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateLoanAccountNoteMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateLoanAccountNoteMapperConfiguration() : base()
        {
            CreateMap<UpdateLoanAccountNoteDto, LoanAccountNote>();
        }
    }
}