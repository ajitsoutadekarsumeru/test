using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PrimaryAllocationStoredProcedureEnum : FlexEnum
    {
        public PrimaryAllocationStoredProcedureEnum() { }

        public PrimaryAllocationStoredProcedureEnum(string value, string displayName) : base(value, displayName) { }

        public static readonly PrimaryAllocationStoredProcedureEnum InsertIntermediateTable =
            new PrimaryAllocationStoredProcedureEnum("accountagencyintermediatetable", "accountagencyintermediatetable");

        public static readonly PrimaryAllocationStoredProcedureEnum ValidateAgency =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationValidationsAgency", "PrimaryAllocationValidationsAgency");

        public static readonly PrimaryAllocationStoredProcedureEnum ValidateTelecallingAgency =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationValidationsTeleCaller", "PrimaryAllocationValidationsTeleCaller");

        public static readonly PrimaryAllocationStoredProcedureEnum ValidateAllocationOwner =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationOwnerValidations", "PrimaryAllocationOwnerValidations");

        public static readonly PrimaryAllocationStoredProcedureEnum InsertIntoMainAndErrorTable =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocInsertIntoMainAndErrorTable", "PrimaryAllocInsertIntoMainAndErrorTable");

        public static readonly PrimaryAllocationStoredProcedureEnum AllocateToAgency =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationToAgency", "PrimaryAllocationToAgency");

        public static readonly PrimaryAllocationStoredProcedureEnum AllocateToTelecallingAgency =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationToTeleCaller", "PrimaryAllocationToTeleCaller");

        public static readonly PrimaryAllocationStoredProcedureEnum AllocateToOwner =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationToAllocationOwner", "PrimaryAllocationToAllocationOwner");

        public static readonly PrimaryAllocationStoredProcedureEnum InsertAllocationHistory =
            new PrimaryAllocationStoredProcedureEnum("InsertPrimaryAllocationHistoryAccounts", "InsertPrimaryAllocationHistoryAccounts");

        public static readonly PrimaryAllocationStoredProcedureEnum CleanupRecords =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationDeleteRecords", "PrimaryAllocationDeleteRecords");

        public static readonly PrimaryAllocationStoredProcedureEnum ExportToCsv =
            new PrimaryAllocationStoredProcedureEnum("PrimaryAllocationExportToCsv", "PrimaryAllocationExportToCsv");
    }
}
