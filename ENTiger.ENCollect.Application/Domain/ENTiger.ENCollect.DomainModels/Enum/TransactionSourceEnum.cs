using Sumeru.Flex;

namespace ENTiger.ENCollect.DomainModels.Enum
{
    public class TransactionSourceEnum : FlexEnum
    {
        public TransactionSourceEnum() { }

        public TransactionSourceEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly TransactionSourceEnum Web = new TransactionSourceEnum("Web", "Web");
        public static readonly TransactionSourceEnum Mobile = new TransactionSourceEnum("Mobile", "Mobile");
        public static readonly TransactionSourceEnum BulkUpload = new TransactionSourceEnum("Bulk Upload", "Bulk Upload");
        public static readonly TransactionSourceEnum API = new TransactionSourceEnum("API", "API");
        public static readonly TransactionSourceEnum System = new TransactionSourceEnum("System", "System");
    }
}
