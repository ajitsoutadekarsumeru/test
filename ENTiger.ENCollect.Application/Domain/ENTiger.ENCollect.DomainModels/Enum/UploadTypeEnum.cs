using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class UploadTypeEnum : FlexEnum
    {
        public UploadTypeEnum()
        { }

        public UploadTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly UploadTypeEnum ImportAccountByAPI = new UploadTypeEnum("ImportAccountsAPI", "ImportAccountsAPI");
        public static readonly UploadTypeEnum ImportAccountByFile = new UploadTypeEnum("AccountImportFile", "AccountImportFile");

        public static readonly UploadTypeEnum Agent = new UploadTypeEnum("agent", "agent");
        public static readonly UploadTypeEnum Agency = new UploadTypeEnum("agency", "agency");
        public static readonly UploadTypeEnum Staff = new UploadTypeEnum("staff", "staff");

        public static readonly UploadTypeEnum CollectionImport = new UploadTypeEnum("CollectionImport", "CollectionImport");
        public static readonly UploadTypeEnum CollectionBulkUpload = new UploadTypeEnum("CollectionBulkUpload", "CollectionBulkUpload");
        public static readonly UploadTypeEnum PrimaryAllocation = new UploadTypeEnum("PrimaryAllocation", "PrimaryAllocation");
        public static readonly UploadTypeEnum SecondaryAllocation = new UploadTypeEnum("SecondaryAllocation", "SecondaryAllocation");

    }
}