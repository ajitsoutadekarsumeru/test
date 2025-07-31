using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AllocationTypeEnum : FlexEnum
    {
        public AllocationTypeEnum()
        { }

        public AllocationTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly AllocationTypeEnum Agency = new AllocationTypeEnum("agency", "agency");
        public static readonly AllocationTypeEnum FieldAgency = new AllocationTypeEnum("fieldagency", "fieldagency");

        public static readonly AllocationTypeEnum TeleCallingAgency = new AllocationTypeEnum("telecallingagency", "telecallingagency");
        public static readonly AllocationTypeEnum ChildAgency = new AllocationTypeEnum("childagency", "childagency");
        public static readonly AllocationTypeEnum AllocationOwner = new AllocationTypeEnum("allocationowner", "allocationowner");
        public static readonly AllocationTypeEnum Agent = new AllocationTypeEnum("agent", "agent");
        public static readonly AllocationTypeEnum FieldAgent = new AllocationTypeEnum("fieldagent", "fieldagent");
        public static readonly AllocationTypeEnum Staff = new AllocationTypeEnum("staff", "staff");
        public static readonly AllocationTypeEnum BankStaff = new AllocationTypeEnum("bankstaff", "bankstaff");
        public static readonly AllocationTypeEnum Telecaller = new AllocationTypeEnum("telecaller", "telecaller");
        public static readonly AllocationTypeEnum TCAgent = new AllocationTypeEnum("tcagent", "tcagent");

        public static readonly AllocationTypeEnum ChildAgent = new AllocationTypeEnum("childagent", "childagent");
        public static readonly AllocationTypeEnum ParentAgent = new AllocationTypeEnum("parentagent", "parentagent");
        public static readonly AllocationTypeEnum ChildTelecallingAgent = new AllocationTypeEnum("childtelecallingagent", "childtelecallingagent");

        public static readonly AllocationTypeEnum Primary = new AllocationTypeEnum("primary", "primary");
        public static readonly AllocationTypeEnum Secondary = new AllocationTypeEnum("secondary", "secondary");


    }
}