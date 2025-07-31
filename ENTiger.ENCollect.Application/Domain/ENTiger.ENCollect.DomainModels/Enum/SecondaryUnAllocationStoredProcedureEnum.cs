using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SecondaryUnAllocationStoredProcedureEnum : FlexEnum
    {
        public SecondaryUnAllocationStoredProcedureEnum() { }

        public SecondaryUnAllocationStoredProcedureEnum(string value, string displayName) : base(value, displayName) { }

        public static readonly SecondaryUnAllocationStoredProcedureEnum InsertIntermediateTable =
          new SecondaryUnAllocationStoredProcedureEnum("SecondaryUnAllocationIntermediate", "SecondaryUnAllocationIntermediate");

        public static readonly SecondaryUnAllocationStoredProcedureEnum ValidateAccount =
            new SecondaryUnAllocationStoredProcedureEnum("SecondaryUnAllocation_Validation", "SecondaryUnAllocation_Validation");

        public static readonly SecondaryUnAllocationStoredProcedureEnum UpdateFileStatus =
            new SecondaryUnAllocationStoredProcedureEnum("SecondaryUnAllocation_UpdateFileStatus", "SecondaryUnAllocation_UpdateFileStatus");

        public static readonly SecondaryUnAllocationStoredProcedureEnum Update =
            new SecondaryUnAllocationStoredProcedureEnum("SecondaryUnAllocation_Update", "SecondaryUnAllocation_Update");

    }
}
