using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PaymentStatusEnum : FlexEnum
    {
        public PaymentStatusEnum()
        { }

        public PaymentStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly PaymentStatusEnum Stab = new PaymentStatusEnum("STAB", "STAB");
        public static readonly PaymentStatusEnum RB = new PaymentStatusEnum("RB", "RB");
        public static readonly PaymentStatusEnum OD = new PaymentStatusEnum("OD", "OD");
        public static readonly PaymentStatusEnum Norm = new PaymentStatusEnum("Norm", "Norm");
        public static readonly PaymentStatusEnum RF = new PaymentStatusEnum("RF", "RF");


    }
}