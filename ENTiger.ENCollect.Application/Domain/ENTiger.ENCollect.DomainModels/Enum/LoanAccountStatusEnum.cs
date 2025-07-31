using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class LoanAccountStatusEnum : FlexEnum
    {
        public LoanAccountStatusEnum()
        { }

        public LoanAccountStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly LoanAccountStatusEnum Live = new LoanAccountStatusEnum("Live", "Live");

    }
}
