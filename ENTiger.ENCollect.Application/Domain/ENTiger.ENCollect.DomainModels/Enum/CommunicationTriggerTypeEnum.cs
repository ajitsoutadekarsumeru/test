using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CommunicationTriggerTypeEnum : FlexEnum
    {
        public CommunicationTriggerTypeEnum()
        { }

        public CommunicationTriggerTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly CommunicationTriggerTypeEnum OnXthDayBeforeDueDate = new CommunicationTriggerTypeEnum("OnXthDayBeforeDueDate", "OnXthDayBeforeDueDate");
        public static readonly CommunicationTriggerTypeEnum OnXthDayAfterSettlementDate = new CommunicationTriggerTypeEnum("OnXthDayAfterSettlementDate", "OnXthDayAfterSettlementDate");
        
        public static readonly CommunicationTriggerTypeEnum OnDPD = new CommunicationTriggerTypeEnum("OnDPD", "OnDPD");
        public static readonly CommunicationTriggerTypeEnum OnPTPDate = new CommunicationTriggerTypeEnum("OnPTPDate", "OnPTPDate");

        public static readonly CommunicationTriggerTypeEnum OnBrokenPTP = new CommunicationTriggerTypeEnum("OnBrokenPTP", "mysOnBrokenPTPql");
        public static readonly CommunicationTriggerTypeEnum OnAgencyAllocationChange = new CommunicationTriggerTypeEnum("mssOnAgencyAllocationChangeql", "OnAgencyAllocationChange");
    }
}