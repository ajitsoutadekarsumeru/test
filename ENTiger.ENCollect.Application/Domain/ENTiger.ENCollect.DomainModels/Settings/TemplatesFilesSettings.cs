namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents template file mappings for different allocation levels.
    /// </summary>
    public class TemplatesFilesSettings
    {
        public string PrimaryAllocation_Agency_AccountLevel { get; set; } = string.Empty;
        public string PrimaryAllocation_Agency_CustomerIdLevel { get; set; } = string.Empty;
        public string PrimaryAllocation_AllocationOwner_AccountLevel { get; set; } = string.Empty;
        public string PrimaryAllocation_AllocationOwner_CustomerIdLevel { get; set; } = string.Empty;
        public string PrimaryAllocation_TelecallingAgency_AccountLevel { get; set; } = string.Empty;
        public string PrimaryAllocation_TelecallingAgency_CustomerIdLevel { get; set; } = string.Empty;
        public string SecondaryAllocation_Agent_AccountLevel { get; set; } = string.Empty;
        public string SecondaryAllocation_Agent_CustomerIdLevel { get; set; } = string.Empty;
        public string SecondaryAllocation_Staff_AccountLevel { get; set; } = string.Empty;
        public string SecondaryAllocation_Staff_CustomerIdLevel { get; set; } = string.Empty;
        public string SecondaryAllocation_Telecaller_AccountLevel { get; set; } = string.Empty;
        public string SecondaryAllocation_Telecaller_CustomerIdLevel { get; set; } = string.Empty;
        public string ExcelTemplateBulkTrail { get; set; } = string.Empty;
        public string ExcelDestinationBulkTrail { get; set; } = string.Empty;
    }
}