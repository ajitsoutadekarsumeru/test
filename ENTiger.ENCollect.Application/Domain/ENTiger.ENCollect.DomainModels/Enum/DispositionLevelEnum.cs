using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class DispositionLevelEnum : FlexEnum
    {
        public DispositionLevelEnum() { }

        public DispositionLevelEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly DispositionLevelEnum AccountLevel = new DispositionLevelEnum("Account Level", "Account Level");
        public static readonly DispositionLevelEnum CustomerLevel = new DispositionLevelEnum("Customer Level", "Customer Level");
    }
}
