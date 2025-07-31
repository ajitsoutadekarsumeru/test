using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SettlementTrancheTypeEnum : FlexEnum
    {
        public SettlementTrancheTypeEnum()
        { }

        public SettlementTrancheTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly SettlementTrancheTypeEnum OneTime = new SettlementTrancheTypeEnum("onetime", "OneTime");

        public static readonly SettlementTrancheTypeEnum Staggered = new SettlementTrancheTypeEnum("staggered", "Staggered");
    }
}