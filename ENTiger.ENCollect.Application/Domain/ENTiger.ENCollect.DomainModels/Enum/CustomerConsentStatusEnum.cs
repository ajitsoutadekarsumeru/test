using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CustomerConsentStatusEnum : FlexEnum
    {
        public CustomerConsentStatusEnum()
        { }

        public CustomerConsentStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly CustomerConsentStatusEnum Pending = new CustomerConsentStatusEnum("pending", "pending");

        public static readonly CustomerConsentStatusEnum Approved = new CustomerConsentStatusEnum("approved", "approved");
        public static readonly CustomerConsentStatusEnum Rejected = new CustomerConsentStatusEnum("rejected", "rejected");
        public static readonly CustomerConsentStatusEnum Expired = new CustomerConsentStatusEnum("expired", "expired");

    }
}