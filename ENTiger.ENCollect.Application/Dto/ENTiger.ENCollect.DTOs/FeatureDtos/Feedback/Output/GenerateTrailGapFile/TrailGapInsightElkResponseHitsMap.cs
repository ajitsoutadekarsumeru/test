using CsvHelper.Configuration;

namespace ENTiger.ENCollect.FeedbackModule
{

    public sealed class TrailGapInsightElkResponseHitsMap : ClassMap<ENTiger.ENCollect.FeedbackModule.TrailGapInsightDownloadRoot.Hits>
    {
        public TrailGapInsightElkResponseHitsMap()
        {

            Map(m => m._source.agency_applicationorg_firstname).Index(0).Name("Field Agency Name"); 
            Map(m => m._source.collector_applicationuser_firstname).Index(1).Name("Field Agent / Staff Name");
            Map(m => m._source.productgroup_loanaccounts).Index(2).Name("Product Group");
            Map(m => m._source.product_loanaccounts).Index(2).Name("Product");
            Map(m => m._source.subproduct_loanaccounts).Index(3).Name("SubProduct");
            Map(m => m._source.current_bucket_loanaccounts).Index(4).Name("CurrentBucket");
            Map(m => m._source.bucket_loanaccounts).Index(5).Name("BOMBucket");
            Map(m => m._source.region_loanaccounts).Index(7).Name("Region");
            Map(m => m._source.state_loanaccounts).Index(8).Name("State");
            Map(m => m._source.city_loanaccounts).Index(9).Name("City");
            Map(m => m._source.branch_loanaccounts).Index(10).Name("Branch");
            Map(m => m._source.agreementid_loanacounts).Index(11).Name("Account No");
            Map(m => m._source.feedback_status).Index(11).Name("Action / Unactioned");
            Map(m => m._source.fbak_dispositiongroup).Index(11).Name("Current Trial Group Status");
            Map(m => m._source.total_overdue_amt).Index(11).Name("Total Outstanding Amount");

        }
    }




}




