using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPrimaryAllocationDetailsELKDto : DtoBridge
    {
        public string? agreementid_loanacounts { get; set; }
        public string? productgroup_loanaccounts { get; set; }
        public string? product_loanaccounts { get; set; }
        public string? subproduct_loanaccounts { get; set; }
        public string? current_bucket_loanaccounts { get; set; }
        public string? bucket_loanaccounts { get; set; }
        public string? zone_loanaccounts { get; set; }
        public string? region_loanaccounts { get; set; }
        public string? state_loanaccounts { get; set; }
        public string? city_loanaccounts { get; set; }
        public string? branch_loanaccounts { get; set; }
        public decimal? bom_pos_loanaccounts { get; set; }

        public decimal? bom_current_pos_loanaccounts { get; set; }

        public decimal? principal_od { get; set; }
        public decimal? interest_od { get; set; }
        public decimal? charge_overdue { get; set; }
        public decimal? total_overdue { get; set; }
        public string? npa_stageid_loanaccounts { get; set; }
        public decimal? amountoutstanding { get; set; }
        public string? allocationowner_applicationuser_firstname { get; set; }
        public string? allocation_owner_role_desig_designation_name { get; set; }
        public string? allocation_owner_code_allocationowner_applicationuser_customid { get; set; }
        public string? telecallingagency_applicationorg_firstname { get; set; }
        public string? telecallingagency_applicationorg_customid { get; set; }
        public string? tellecaller_applicationuser_firstname { get; set; }
        public string? tellecaller_applicationuser_customid { get; set; }
        public string? agency_applicationorg_firstname { get; set; }
        public string? agency_applicationorg_customid { get; set; }
        public string? collector_applicationuser_firstname { get; set; }
        public string? collector_applicationuser_customid { get; set; }
        public string? primaryallocationstatusfortelecallingagency { get; set; }
        public string? primaryallocationstatusforfieldagency { get; set; }
        public string? secondaryallocationstatusfortelecallingagent { get; set; }
        public string? secondaryallocationstatusforfieldagent { get; set; }
        public string? primaryallocationstatus { get; set; }
        public string? secondaryallocationstatus { get; set; }
        public string? lastmodifieddate_loanaccounts { get; set; }

    }
}
