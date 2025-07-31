using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class DBTypeEnum : FlexEnum
    {
        public DBTypeEnum()
        { }

        public DBTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly DBTypeEnum MySQL = new DBTypeEnum("mysql", "mysql");

        public static readonly DBTypeEnum MsSQL = new DBTypeEnum("mssql", "mssql");
    }
}