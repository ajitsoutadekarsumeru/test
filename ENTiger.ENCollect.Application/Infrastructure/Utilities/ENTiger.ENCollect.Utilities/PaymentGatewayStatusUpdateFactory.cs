using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayStatusUpdateFactory : IFlexUtilityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<PaymentGatewayEnum, Type> _paymentGatewayMappings;
        public PaymentGatewayStatusUpdateFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            //Map the Enum strings witht he Concrete class types
            _paymentGatewayMappings = new Dictionary<PaymentGatewayEnum, Type>
            {
                { PaymentGatewayEnum.RazorPay, typeof(PaymentGatewayRazorPay) }
            };
        }
        public virtual IPaymentGateway GetPaymentGateway(PaymentGatewayEnum paymentGatewayType)
        {
            if (_paymentGatewayMappings.TryGetValue(paymentGatewayType, out var serviceType))
            {
                return (IPaymentGateway)_serviceProvider.GetRequiredService(serviceType);
            }

            throw new InvalidOperationException("Invalid Payment Gateway");
        }
    }
}
