using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountAccessScopeEnum : FlexEnum
    {
        public AccountAccessScopeEnum()
        { }

        public AccountAccessScopeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly AccountAccessScopeEnum All = new AccountAccessScopeEnum("1", "all");

        public static readonly AccountAccessScopeEnum Parent = new AccountAccessScopeEnum("2", "parent");
        public static readonly AccountAccessScopeEnum Self = new AccountAccessScopeEnum("3", "self");
    }
}