using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AgencySubTypeEnum : FlexEnum
    {
        public AgencySubTypeEnum()
        { }

        public AgencySubTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly AgencySubTypeEnum TeleCalling = new AgencySubTypeEnum("Tele calling", "Tele calling");
        public static readonly AgencySubTypeEnum FieldAgent = new AgencySubTypeEnum("Field Agents", "Field Agents");



    }
}