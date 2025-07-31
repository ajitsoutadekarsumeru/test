using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountUserTypeScopeEnum : FlexEnum
    {
        public AccountUserTypeScopeEnum()
        { }

        public AccountUserTypeScopeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly AccountUserTypeScopeEnum FieldAgent = new AccountUserTypeScopeEnum("FieldAgent", "FieldAgent");
        public static readonly AccountUserTypeScopeEnum FieldTCAgent = new AccountUserTypeScopeEnum("FieldTCAgent", "FieldTCAgent");
        public static readonly AccountUserTypeScopeEnum BankStaff = new AccountUserTypeScopeEnum("BankStaff", "BankStaff");
        public static readonly AccountUserTypeScopeEnum BankTCStaff = new AccountUserTypeScopeEnum("BankTCStaff", "BankTCStaff");
    }
}