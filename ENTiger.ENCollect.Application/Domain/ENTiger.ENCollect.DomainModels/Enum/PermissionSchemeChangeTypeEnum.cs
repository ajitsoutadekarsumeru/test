namespace ENTiger.ENCollect
{
    public class PermissionSchemeChangeTypeEnum : FlexEnum
    {
        public PermissionSchemeChangeTypeEnum()
        { }

        public PermissionSchemeChangeTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly PermissionSchemeChangeTypeEnum Create = new PermissionSchemeChangeTypeEnum("Create", "Create");
        public static readonly PermissionSchemeChangeTypeEnum Edit = new PermissionSchemeChangeTypeEnum("Edit", "Edit");
    }
}
