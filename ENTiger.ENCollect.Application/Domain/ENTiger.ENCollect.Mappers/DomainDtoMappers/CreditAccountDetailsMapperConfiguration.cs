using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreditAccountDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CreditAccountDetailsMapperConfiguration() : base()
        {
            CreateMap<CreditAccountDetailsDto, CreditAccountDetails>();
            CreateMap<CreditAccountDetails, CreditAccountDetailsDto>();
            CreateMap<CreditAccountDetailsDtoWithId, CreditAccountDetails>();
            CreateMap<CreditAccountDetails, CreditAccountDetailsDtoWithId>();
        }
    }
}