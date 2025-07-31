using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAllocationAchievedSummaryDto : DtoBridge
    {
        public string? CollectionStatus { get; set; }

        public string? Loan_Bucket { get; set; }

        public string? ProductGroup { get; set; }

        public string? Product { get; set; }

        public string? Subproduct { get; set; }

        public string? Current_Bucket { get; set; }

        public string? Bucket { get; set; }

        public string? Region { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Branch { get; set; }

        public string? FieldAgencyId { get; set; }

        public string? FieldAgencyName { get; set; }

        public string? TelecallingAgencyId { get; set; }

        public string? TelecallingAgencyName { get; set; }

        public string? FieldAgentId { get; set; }

        public string? FieldAgentName { get; set; }

        public string? TelecallerId { get; set; }

        public string? TelecallerName { get; set; }

        public string? Field_Discriminator { get; set; }

        public string? Total_Accounts { get; set; }

        public string? Total_Overdue_Amount { get; set; }


    }
}
