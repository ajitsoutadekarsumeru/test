using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class MasterEnum : FlexEnum
    {
        public MasterEnum()
        { }

        public MasterEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly MasterEnum BASEBRANCHMASTER = new MasterEnum("BASEBRANCHMASTER", "BASEBRANCHMASTER");
        public static readonly MasterEnum PRODUCTMASTER = new MasterEnum("PRODUCTMASTER", "PRODUCTMASTER");
        public static readonly MasterEnum DEPOSITBANKMASTER = new MasterEnum("DEPOSITBANKMASTER", "DEPOSITBANKMASTER");
        public static readonly MasterEnum DISPOSITIONCODEMASTER = new MasterEnum("DISPOSITIONCODEMASTER", "DISPOSITIONCODEMASTER");
        public static readonly MasterEnum BUCKETMASTER = new MasterEnum("BUCKETMASTER", "BUCKETMASTER");
        public static readonly MasterEnum BANKMASTER = new MasterEnum("BANKMASTER", "BANKMASTER");
        public static readonly MasterEnum GEOMASTER = new MasterEnum("GEOMASTER", "GEOMASTER");
        public static readonly MasterEnum DEPARTMENTDESIGNATIONMASTER = new MasterEnum("DEPARTMENTDESIGNATIONMASTER", "DEPARTMENTDESIGNATIONMASTER");


    }
}