using Sumeru.Flex;

namespace ENTiger.ENCollect.DomainModels.Enum
{
    public class TransactionTypeEnum : FlexEnum
    {
        public TransactionTypeEnum() { }

        public TransactionTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly TransactionTypeEnum Trail = new TransactionTypeEnum("Trail", "Trail");
        public static readonly TransactionTypeEnum Receipt = new TransactionTypeEnum("Receipt", "Receipt");
        public static readonly TransactionTypeEnum Login = new TransactionTypeEnum("Login", "Login");
        public static readonly TransactionTypeEnum Logout = new TransactionTypeEnum("Logout", "Logout");
    }
}
