using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AuditOperationEnum : FlexEnum
    {
        public AuditOperationEnum()
        { }

        public AuditOperationEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly AuditOperationEnum Add = new AuditOperationEnum("Add", "Add");
        public static readonly AuditOperationEnum Edit = new AuditOperationEnum("Edit", "Edit");
        public static readonly AuditOperationEnum Delete = new AuditOperationEnum("Delete", "Delete");
        public static readonly AuditOperationEnum Upload = new AuditOperationEnum("Upload", "Upload");
        public static readonly AuditOperationEnum Download = new AuditOperationEnum("Download", "Download");
    }
}
