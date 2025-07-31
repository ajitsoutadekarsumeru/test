using CsvHelper.Configuration;

namespace ENTiger.ENCollect.AllocationModule;


public sealed class SecondaryAllocationInsightElkResponseHitsMap : ClassMap<ENTiger.ENCollect.AllocationModule.SecondaryAllocationInsightDownloadRoot.Hits>
{
    public SecondaryAllocationInsightElkResponseHitsMap()
    {

        Map(m => m._source.agreementid_loanacounts).Index(0).Name("AccountNo"); 
        Map(m => m._source.productgroup_loanaccounts).Index(1).Name("ProductGroup");
        Map(m => m._source.product_loanaccounts).Index(2).Name("Product");
        Map(m => m._source.subproduct_loanaccounts).Index(3).Name("SubProduct");
        Map(m => m._source.current_bucket_loanaccounts).Index(4).Name("CurrentBucket");
        Map(m => m._source.bucket_loanaccounts).Index(5).Name("BOMBucket");
        Map(m => m._source.zone_loanaccounts).Index(6).Name("Zone");
        Map(m => m._source.region_loanaccounts).Index(7).Name("Region");

        Map(m => m._source.state_loanaccounts).Index(8).Name("State");
        Map(m => m._source.city_loanaccounts).Index(9).Name("City");

        Map(m => m._source.branch_loanaccounts).Index(10).Name("Branch");
        Map(m => m._source.bom_pos_loanaccounts).Index(11).Name("BOM_POS");
        Map(m => m._source.bom_pos_loanaccounts).Index(12).Name("BOM_POS");
        Map(m => m._source.principal_od).Index(13).Name("PrincipalOverdue");
        Map(m => m._source.interest_od).Index(14).Name("InterestOverdue");
        Map(m => m._source.charge_overdue).Index(15).Name("ChargeOverdue");
        Map(m => m._source.total_overdue).Index(16).Name("TotalOverdue");
        Map(m => m._source.npa_stageid_loanaccounts).Index(17).Name("NPA_Flag");
        Map(m => m._source.amountoutstanding).Index(18).Name("AmountOutstanding");
        Map(m => m._source.allocationowner_applicationuser_firstname).Name("AllocationOwnerName");
        Map(m => m._source.allocation_owner_role_desig_designation_name).Name("AllocationOwnerRole");
        Map(m => m._source.allocation_owner_code_allocationowner_applicationuser_customid).Name("AllocationOwnerCustom_ID");
        Map(m => m._source.telecallingagency_applicationorg_firstname).Name("Telecalling_Agency_Name");
        Map(m => m._source.telecallingagency_applicationorg_customid).Name("Telecalling_Agency_Custom_ID"); 
        Map(m => m._source.agency_applicationorg_firstname).Name("agency_applicationorg_firstname");
        Map(m => m._source.agency_applicationorg_customid).Name("agency_applicationorg_customid");
        Map(m => m._source.secondaryallocationstatusfortelecallingagent).Name("Secondary_Alloc_Status_For_Telecalling_Agency");
        Map(m => m._source.secondaryallocationstatusforfieldagent).Name("Secondary_Alloc_Status_For_Field_Agency");
        Map(m => m._source.primaryallocationstatus).Name("Primary_Allocation_Status");
        Map(m => m._source.secondaryallocationstatus).Name("Secondary_Allocation_Status");
        Map(m => m._source.lastmodifieddate_loanaccounts).Name("LA_Lastmodified");
        Map(m => m._source.total_overdue).Name("LA_Lastmodified_Date_And_Time");
        
    }
}




