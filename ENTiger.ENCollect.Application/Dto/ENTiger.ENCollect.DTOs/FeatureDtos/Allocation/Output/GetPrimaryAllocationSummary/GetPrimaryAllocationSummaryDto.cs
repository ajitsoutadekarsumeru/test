using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPrimaryAllocationSummaryDto : DtoBridge
    {
        public string? Account_Allocation_Status { get; set; }

        public string? Entity { get; set; }

        public string? Allocation_Owner_Name { get; set; }

        public string? Allocation_Owner_Id { get; set; }

        public string? Current_Bucket { get; set; }

        public string? Loan_Bucket { get; set; }

        public string? Region { get; set; }

        public string? State { get; set; }
        public string? City { get; set; }

        public string? Branch { get; set; }

        public string? Product { get; set; }
        public string? ProductGroup { get; set; }
        public string? SubProduct { get; set; }

        public string? Field_Agency_Name { get; set; }

        public string? Field_Agency_Id { get; set; }

        public string? Telecalling_Agency_Name { get; set; }

        public string? Telecalling_Agency_Id { get; set; }

        public string? Bom_Bucket { get; set; }

        public string? Field_Discriminator { get; set; }

        public string? Total_Accounts { get; set; }

        public string? Total_Prinicpal_Overdue { get; set; }

        public string? Total_Overdue_Amount { get; set; }

    }
}
