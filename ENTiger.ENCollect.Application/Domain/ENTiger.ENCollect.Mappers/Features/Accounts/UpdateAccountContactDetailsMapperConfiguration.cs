using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateAccountContactDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateAccountContactDetailsMapperConfiguration() : base()
        {
            CreateMap<UpdateAccountContactDetailsDto, LoanAccount>();
        }
    }
}