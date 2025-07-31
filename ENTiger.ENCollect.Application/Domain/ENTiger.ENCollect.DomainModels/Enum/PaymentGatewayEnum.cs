using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayEnum : FlexEnum
    {
        public PaymentGatewayEnum()
        { }

        public PaymentGatewayEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly PaymentGatewayEnum Paynimo = new PaymentGatewayEnum("paynimo", "paynimo");

        public static readonly PaymentGatewayEnum RazorPay = new PaymentGatewayEnum("razorpay", "razorpay");

        public static readonly PaymentGatewayEnum PayU = new PaymentGatewayEnum("payu", "payu");

        public static readonly PaymentGatewayEnum Billdesk = new PaymentGatewayEnum("billdesk", "billdesk");

        public static readonly PaymentGatewayEnum PayUSMS = new PaymentGatewayEnum("payusms", "payusms");

        public static readonly PaymentGatewayEnum PayUEmail = new PaymentGatewayEnum("payuemail", "payuemail");

        public static readonly PaymentGatewayEnum PayUTimeInMins = new PaymentGatewayEnum("payutimeinmins", "payutimeinmins");

        public static readonly PaymentGatewayEnum PayUSource = new PaymentGatewayEnum("payusource", "payusource");
    }
}