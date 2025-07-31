using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PaymentGatewayMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PaymentGatewayMapperConfiguration() : base()
        {
            CreateMap<PaymentGatewayDto, PaymentGateway>();
            CreateMap<PaymentGateway, PaymentGatewayDto>();
            CreateMap<PaymentGatewayDtoWithId, PaymentGateway>();
            CreateMap<PaymentGateway, PaymentGatewayDtoWithId>();
        }
    }
}