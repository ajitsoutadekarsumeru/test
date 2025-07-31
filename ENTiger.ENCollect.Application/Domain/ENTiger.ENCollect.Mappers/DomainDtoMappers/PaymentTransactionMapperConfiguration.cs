using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PaymentTransactionMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PaymentTransactionMapperConfiguration() : base()
        {
            CreateMap<PaymentTransactionDto, PaymentTransaction>();
            CreateMap<PaymentTransaction, PaymentTransactionDto>();
            CreateMap<PaymentTransactionDtoWithId, PaymentTransaction>();
            CreateMap<PaymentTransaction, PaymentTransactionDtoWithId>();
        }
    }
}