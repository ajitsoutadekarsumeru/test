using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class DispCodeEnum : FlexEnum
    {
        public DispCodeEnum()
        { }

        public DispCodeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly ProductCodeEnum BPTP = new ProductCodeEnum("bptp", "bptp");
        public static readonly ProductCodeEnum PTP = new ProductCodeEnum("ptp", "ptp");
        public static readonly ProductCodeEnum PTPGroup = new ProductCodeEnum("ptp Group", "ptp Group");

    }
}