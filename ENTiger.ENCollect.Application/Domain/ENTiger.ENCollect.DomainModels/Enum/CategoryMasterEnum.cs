using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CategoryMasterEnum : FlexEnum
    {
        public CategoryMasterEnum()
        { }

        public CategoryMasterEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly CategoryMasterEnum TelecallerConnect = new CategoryMasterEnum("TelecallerConnect", "TelecallerConnect");
        public static readonly CategoryMasterEnum TelecallerContact = new CategoryMasterEnum("TelecallerContact", "TelecallerContact");
        public static readonly CategoryMasterEnum TelecallerWorkableAccounts = new CategoryMasterEnum("TelecallerWorkableAccounts", "TelecallerWorkableAccounts");
        public static readonly CategoryMasterEnum TelecallerPTPGroup = new CategoryMasterEnum("TelecallerPTPGroup", "TelecallerPTPGroup");

        public static readonly CategoryMasterEnum PrimaryAllocationHeaders = new CategoryMasterEnum("PrimaryAllocationHeaders", "PrimaryAllocationHeaders");
        public static readonly CategoryMasterEnum SecondaryAllocationHeaders = new CategoryMasterEnum("SecondaryAllocationHeaders", "SecondaryAllocationHeaders");
        public static readonly CategoryMasterEnum SubProduct = new CategoryMasterEnum("SubProduct", "SubProduct");
        public static readonly CategoryMasterEnum ImportAccountsHeaders = new CategoryMasterEnum("ImportAccountsHeaders", "ImportAccountsHeaders");
        public static readonly CategoryMasterEnum ProductGroup = new CategoryMasterEnum("ProductGroup", "ProductGroup");
        public static readonly CategoryMasterEnum Product = new CategoryMasterEnum("Product", "Product");

        public static readonly CategoryMasterEnum UsersCreateHeaders = new CategoryMasterEnum("UsersCreateHeaders", "UsersCreateHeaders");
        public static readonly CategoryMasterEnum MasterUpdateHeaders = new CategoryMasterEnum("MasterUpdateHeaders", "MasterUpdateHeaders");
        public static readonly CategoryMasterEnum UsersUpdateHeaders = new CategoryMasterEnum("UsersUpdateHeaders", "UsersUpdateHeaders");

    }
}