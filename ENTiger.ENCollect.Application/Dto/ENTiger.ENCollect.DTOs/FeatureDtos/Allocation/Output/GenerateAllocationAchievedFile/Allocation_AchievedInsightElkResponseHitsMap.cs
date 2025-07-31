using CsvHelper.Configuration;

namespace ENTiger.ENCollect.FeedbackModule
{

    public sealed class Allocation_AchievedInsightElkResponseHitsMap : ClassMap<ENTiger.ENCollect.AllocationModule.AllocationAchievedInsightDownloadRoot.Hits>
    {
        public Allocation_AchievedInsightElkResponseHitsMap()
        {

            Map(m => m._source.agreementid_loanacounts).Name("AccountNo");
            Map(m => m._source.productgroup_loanaccounts).Name("ProductGroup");
            Map(m => m._source.product_loanaccounts).Name("Product");
            Map(m => m._source.subproduct_loanaccounts).Name("SubProduct");
            Map(m => m._source.current_bucket_loanaccounts).Name("CurrentBucket");
            Map(m => m._source.bucket_loanaccounts).Name("BOMBucket");
            Map(m => m._source.zone_loanaccounts).Name("Zone");
            Map(m => m._source.region_loanaccounts).Name("Region");

            Map(m => m._source.state_loanaccounts).Name("State");
            Map(m => m._source.city_loanaccounts).Name("City");

            Map(m => m._source.branch_loanaccounts).Name("Branch");
            Map(m => m._source.bom_pos_loanaccounts).Name("BOM_POS");
            Map(m => m._source.bom_pos_loanaccounts).Name("BOM_POS");
            Map(m => m._source.principal_od).Name("PrincipalOverdue");
            Map(m => m._source.interest_od).Name("InterestOverdue");
            Map(m => m._source.charge_overdue).Name("ChargeOverdue");
            Map(m => m._source.total_overdue).Name("TotalOverdue");
            Map(m => m._source.npa_stageid_loanaccounts).Name("NPA_Flag");
            Map(m => m._source.amountoutstanding).Name("AmountOutstanding");
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
            Map(m => m._source.collection_status).Name("Collected/Uncollected");
        }
    }




}




