using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PrimaryUnAllocationStoredProcedureEnum : FlexEnum
    {
        public PrimaryUnAllocationStoredProcedureEnum() { }

        public PrimaryUnAllocationStoredProcedureEnum(string value, string displayName) : base(value, displayName) { }

        public static readonly PrimaryUnAllocationStoredProcedureEnum InsertIntermediateTable =
          new PrimaryUnAllocationStoredProcedureEnum("PrimaryUnAllocationIntermediate", "PrimaryUnAllocationIntermediate");

        public static readonly PrimaryUnAllocationStoredProcedureEnum ValidateAccount =
            new PrimaryUnAllocationStoredProcedureEnum("PrimaryUnAllocation_Validation", "PrimaryUnAllocation_Validation");

        public static readonly PrimaryUnAllocationStoredProcedureEnum UpdateFileStatus =
            new PrimaryUnAllocationStoredProcedureEnum("PrimaryUnAllocation_UpdateFileStatus", "PrimaryUnAllocation_UpdateFileStatus");

        public static readonly PrimaryUnAllocationStoredProcedureEnum Update =
            new PrimaryUnAllocationStoredProcedureEnum("PrimaryUnAllocation_Update", "PrimaryUnAllocation_Update");

    }
}
