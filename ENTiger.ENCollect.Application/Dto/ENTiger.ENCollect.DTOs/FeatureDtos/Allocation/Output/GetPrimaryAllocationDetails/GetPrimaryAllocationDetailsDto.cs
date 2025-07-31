using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPrimaryAllocationDetailsDto : DtoBridge
    {
        public string? AccountNo { get; set; }
        public string? ProductGroup { get; set; }

        public string? Product { get; set; }
        public string? SubProduct { get; set; }

        public string? CurrentBucket { get; set; }

        public string? BOMBucket { get; set; }

        public string? Zone { get; set; }

        public string? Region { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Branch { get; set; }

        public decimal? BOM_POS { get; set; }

        public decimal? Current_POS { get; set; }

        public decimal? Principal_Overdue { get; set; }

        public decimal? Interest_Overdue { get; set; }

        public decimal? Charge_Overdue { get; set; }

        public decimal? Total_Overdue { get; set; }

        public string? NPA_Flag { get; set; }

        public decimal? Amount_Outstanding { get; set; }

        public string? Allocation_Owner_Name { get; set; }

        public string? Allocation_Owner_Role { get; set; }

        public string? Allocation_Owner_Custom_ID { get; set; }

        public string? Telecalling_Agency_Name { get; set; }

        public string? Telecalling_Agency_Custom_ID { get; set; }

        public string? Field_Agency_Name { get; set; }

        public string? Field_Agency_Custom_ID { get; set; }

        public string? Primary_Alloc_Status_For_Telecalling_Agency { get; set; }

        public string? Primary_Alloc_Status_For_Field_Agency { get; set; }

        public string? Primary_Allocation_Status { get; set; }

        public string? Secondary_Allocation_Status { get; set; }

        public string? LA_Lastmodified { get; set; }

        public string? LA_Lastmodified_Date_And_Time { get; set; }

    }
}
