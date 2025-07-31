namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTrailGapDetailsELKDto : DtoBridge
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
        public string? agency_applicationorg_id { get; set; }
        public string? agency_applicationorg_firstname { get; set; }
        public string? collector_applicationuser_id { get; set; }
        public string? collector_applicationuser_firstname { get; set; }
        public string? feedback_status { get; set; }

        public string? fbak_dispositiongroup { get; set; }

        public decimal? total_overdue_amt { get; set; }


    }

}   