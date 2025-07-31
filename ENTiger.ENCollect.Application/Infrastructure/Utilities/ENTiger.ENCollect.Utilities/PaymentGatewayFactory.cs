using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayFactory : IFlexUtilityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<PaymentGatewayEnum, Type> _paymentGatewayMappings;

        public PaymentGatewayFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            //Map the Enum strings witht he Concrete class types
            _paymentGatewayMappings = new Dictionary<PaymentGatewayEnum, Type>
            {
                { PaymentGatewayEnum.Paynimo, typeof(PaymentGatewayPaynimo) },
                { PaymentGatewayEnum.RazorPay, typeof(PaymentGatewayRazorPay) },
                { PaymentGatewayEnum.PayU, typeof(PaymentGatewayPayu) }
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

        public virtual IPaymentGateway GetPaymentGateway(string paymentGateway)
        {
            return paymentGateway switch
            {
                // FlexContainer.ServiceProvider.
                "paynimo" => _serviceProvider.GetRequiredService<PaymentGatewayPaynimo>(),
                "razorpay" => _serviceProvider.GetRequiredService<PaymentGatewayRazorPay>(),
                "payu" => _serviceProvider.GetRequiredService<PaymentGatewayPayu>(),
                _ => throw new InvalidOperationException("Invalid Payment gateway")
            };
        }
    }
}