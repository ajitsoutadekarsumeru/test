using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExcelFilterTypeEnum : FlexEnum
    {
        public ExcelFilterTypeEnum() { }

        public ExcelFilterTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly ExcelFilterTypeEnum AccountUpload = new ExcelFilterTypeEnum("AccountNo", "AccountNo");
        public static readonly ExcelFilterTypeEnum CustomerUpload = new ExcelFilterTypeEnum("CustomerId", "CustomerId");
        public static readonly ExcelFilterTypeEnum CollectionUpload = new ExcelFilterTypeEnum("AccountNumber", "AccountNumber");
        public static readonly ExcelFilterTypeEnum BulkTrailUpload = new ExcelFilterTypeEnum("Agreement id", "Agreement id");
        public static readonly ExcelFilterTypeEnum AccountImportUpload = new ExcelFilterTypeEnum("AGREEMENTID", "AGREEMENTID");
    }
}
