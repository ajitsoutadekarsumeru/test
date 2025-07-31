using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTrailGapSummaryDto : DtoBridge
    {
        public string? ProductGroup { get; set; }

        public string? Product { get; set; }

        public string? SubProduct { get; set; }

        public string? Current_Bucket { get; set; }

        public string? Bom_Bucket { get; set; }

        public string? Region { get; set; }

        public string? State { get; set; }
        public string? City { get; set; }

        public string? Branch { get; set; }

        public string? Field_Agency_Name { get; set; }

        public string? Field_Agency_Id { get; set; }

        public string? Telecalling_Agency_Name { get; set; }

        public string? Telecalling_Agency_Id { get; set; }

        public string? Collector_Name { get; set; }

        public string? Collector_Id { get; set; }

        public string? Telecaller_Id { get; set; }

        public string? Telecaller_Name { get; set; }

        public string? Field_Discriminator { get; set; }

        public string? Current_Trail_Group { get; set; }

        public string? Current_Trail_Code { get; set; }

        public string? TrailCount { get; set; }

        public string? Total_Accounts { get; set; }

        public string? Total_Overdue_Amount { get; set; }

        public string? Status { get; set; }

        public string? Loan_Bucket { get; set; }

    }
}
