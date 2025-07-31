using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SubProductEnum : FlexEnum
    {
        public SubProductEnum()
        { }

        public SubProductEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly SubProductEnum All = new SubProductEnum("all", "all");


    }
}