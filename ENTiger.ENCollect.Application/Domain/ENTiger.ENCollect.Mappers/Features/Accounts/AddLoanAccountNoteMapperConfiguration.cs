using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddLoanAccountNoteMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddLoanAccountNoteMapperConfiguration() : base()
        {
            CreateMap<AddLoanAccountNoteDto, LoanAccountNote>()
            .ForMember(o => o.LoanAccountId, opt => opt.MapFrom(o => o.AccountId));
        }
    }
}