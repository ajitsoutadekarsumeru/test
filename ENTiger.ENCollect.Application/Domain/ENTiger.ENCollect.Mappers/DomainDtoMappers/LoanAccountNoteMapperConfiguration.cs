using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountNoteMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LoanAccountNoteMapperConfiguration() : base()
        {
            CreateMap<LoanAccountNoteDto, LoanAccountNote>();
            CreateMap<LoanAccountNote, LoanAccountNoteDto>();
            CreateMap<LoanAccountNoteDtoWithId, LoanAccountNote>();
            CreateMap<LoanAccountNote, LoanAccountNoteDtoWithId>();
        }
    }
}