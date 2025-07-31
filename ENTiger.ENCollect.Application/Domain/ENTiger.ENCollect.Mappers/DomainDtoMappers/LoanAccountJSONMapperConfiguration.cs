using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountJSONMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LoanAccountJSONMapperConfiguration() : base()
        {
            CreateMap<LoanAccountJSONDto, LoanAccountJSON>();
            CreateMap<LoanAccountJSON, LoanAccountJSONDto>();
            CreateMap<LoanAccountJSONDtoWithId, LoanAccountJSON>();
            CreateMap<LoanAccountJSON, LoanAccountJSONDtoWithId>();
        }
    }
}