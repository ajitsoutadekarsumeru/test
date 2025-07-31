using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class WaiverCargeTypeEnum : FlexEnum
    {
        public WaiverCargeTypeEnum()
        { }

        public WaiverCargeTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly WaiverCargeTypeEnum Principal = new WaiverCargeTypeEnum("Total Principal Outstanding", "principalOutstanding");

        public static readonly WaiverCargeTypeEnum Interest = new WaiverCargeTypeEnum("Total Interest Outstanding", "interestOutstanding");
        public static readonly WaiverCargeTypeEnum Charge = new WaiverCargeTypeEnum("Charges Outstanding", "chargesOutstanding");

    }
}