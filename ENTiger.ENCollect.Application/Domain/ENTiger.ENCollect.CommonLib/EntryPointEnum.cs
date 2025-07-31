using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class EntryPointEnum : FlexEnum
    {
        public EntryPointEnum()
        { }

        public EntryPointEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly EntryPointEnum Account = new EntryPointEnum("Account", "Account");

        public static readonly EntryPointEnum User = new EntryPointEnum("User", "User");
    }
}