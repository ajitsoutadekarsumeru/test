using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SecondaryAllocationStoredProcedureEnum : FlexEnum
    {
        public SecondaryAllocationStoredProcedureEnum() { }

        public SecondaryAllocationStoredProcedureEnum(string value, string displayName) : base(value, displayName) { }

        public static readonly SecondaryAllocationStoredProcedureEnum InsertIntermediateTable =
            new SecondaryAllocationStoredProcedureEnum("accountagentintermediatetable", "accountagentintermediatetable");

        public static readonly SecondaryAllocationStoredProcedureEnum ValidateAgent =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationValidationsAgent", "SecondaryAllocationValidationsAgent");

        public static readonly SecondaryAllocationStoredProcedureEnum ValidateTelecaller =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationValidations", "SecondaryAllocationValidations");

        public static readonly SecondaryAllocationStoredProcedureEnum ValidateStaff =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationValidationsCompanyUser", "SecondaryAllocationValidationsCompanyUser");

        public static readonly SecondaryAllocationStoredProcedureEnum InsertIntoMainAndErrorTable =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocInsertIntoMainAndErrorTable", "SecondaryAllocInsertIntoMainAndErrorTable");

        public static readonly SecondaryAllocationStoredProcedureEnum AllocateToAgencyUser =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationToAgencyUser", "SecondaryAllocationToAgencyUser");

        public static readonly SecondaryAllocationStoredProcedureEnum AllocateToTelecaller =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationTelecaller", "SecondaryAllocationTelecaller");

        public static readonly SecondaryAllocationStoredProcedureEnum AllocateToStaff =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationToStaff", "SecondaryAllocationToStaff");

        public static readonly SecondaryAllocationStoredProcedureEnum InsertAllocationHistory =
            new SecondaryAllocationStoredProcedureEnum("InsertSecondaryAllocationHistoryAccounts", "InsertSecondaryAllocationHistoryAccounts");

        public static readonly SecondaryAllocationStoredProcedureEnum CleanupRecords =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationDeleteRecords", "SecondaryAllocationDeleteRecords");

        public static readonly SecondaryAllocationStoredProcedureEnum ExportToCsv =
            new SecondaryAllocationStoredProcedureEnum("SecondaryAllocationExportToCsv", "SecondaryAllocationExportToCsv");
    }
}
