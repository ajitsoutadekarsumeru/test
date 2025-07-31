using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class AuditedEntityTypeEnum : FlexEnum
    {
        public AuditedEntityTypeEnum()
        { }

        public AuditedEntityTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly AuditedEntityTypeEnum Collection = new AuditedEntityTypeEnum("Collection", "Collection");
        public static readonly AuditedEntityTypeEnum Allocation = new AuditedEntityTypeEnum("Allocation", "Allocation");
        public static readonly AuditedEntityTypeEnum Agency = new AuditedEntityTypeEnum("Agency", "Agency");
        public static readonly AuditedEntityTypeEnum Agent = new AuditedEntityTypeEnum("Agent", "Agent");
        public static readonly AuditedEntityTypeEnum Staff = new AuditedEntityTypeEnum("Staff", "Staff");
        public static readonly AuditedEntityTypeEnum CollectionBatch = new AuditedEntityTypeEnum("CollectionBatch", "CollectionBatch");
        public static readonly AuditedEntityTypeEnum PayinSlip = new AuditedEntityTypeEnum("PayinSlip", "PayinSlip");
        public static readonly AuditedEntityTypeEnum Accounts = new AuditedEntityTypeEnum("Accounts", "Accounts");
        public static readonly AuditedEntityTypeEnum Communication = new AuditedEntityTypeEnum("Communication", "Communication");
        public static readonly AuditedEntityTypeEnum Trails = new AuditedEntityTypeEnum("Trails", "Trails");
        public static readonly AuditedEntityTypeEnum Master = new AuditedEntityTypeEnum("Master", "Master");
        public static readonly AuditedEntityTypeEnum AccountSearchScope = new AuditedEntityTypeEnum("AccountSearchScope", "Account Search Scope");
        public static readonly AuditedEntityTypeEnum Login = new AuditedEntityTypeEnum("Login", "Login");

        public static readonly AuditedEntityTypeEnum PrimaryBulkAllocation = new AuditedEntityTypeEnum("PrimaryBulkAllocation", "Primary Bulk Allocation");
        public static readonly AuditedEntityTypeEnum PrimaryBulkDeAllocation = new AuditedEntityTypeEnum("PrimaryBulkDeAllocation", "Primary Bulk DeAllocation");

        public static readonly AuditedEntityTypeEnum SecondaryBulkAllocation = new AuditedEntityTypeEnum("SecondaryBulkAllocation", "Secondary Bulk Allocation");
        public static readonly AuditedEntityTypeEnum SecondaryBulkDeAllocation = new AuditedEntityTypeEnum("SecondaryBulkDeAllocation", "Secondary Bulk DeAllocation");
        public static readonly AuditedEntityTypeEnum PermissionScheme = new AuditedEntityTypeEnum("PermissionScheme", "Permission Scheme");
    }
}
