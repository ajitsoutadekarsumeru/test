using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountabilityTypeEnum : FlexEnum
    {
        public AccountabilityTypeEnum()
        { }

        public AccountabilityTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly AccountabilityTypeEnum BankToBackEndInternalBIHP = new AccountabilityTypeEnum("BankToBackEndInternalBIHP", "BankToBackEndInternalBIHP");
        public static readonly AccountabilityTypeEnum AgencyToFrontEndExternalTC = new AccountabilityTypeEnum("AgencyToFrontEndExternalTC", "AgencyToFrontEndExternalTC");
        public static readonly AccountabilityTypeEnum AgencyToFrontEndExternalFOS = new AccountabilityTypeEnum("AgencyToFrontEndExternalFOS", "AgencyToFrontEndExternalFOS");

        public static readonly AccountabilityTypeEnum BankToFrontEndInternalFOS = new AccountabilityTypeEnum("BankToFrontEndInternalFOS", "BankToFrontEndInternalFOS");
        public static readonly AccountabilityTypeEnum BankToFrontEndInternalTC = new AccountabilityTypeEnum("BankToFrontEndInternalTC", "BankToFrontEndInternalTC");



    }
}