using CsvHelper.Configuration.Attributes;

namespace ENTiger.ENCollect.FeedbackModule.TrailGapInsightDownloadRoot
{
    
    public class Shards
    {
        public int? total { get; set; }
        public int? successful { get; set; }
        public int? skipped { get; set; }
        public int? failed { get; set; }
    }

    public class Total
    {
        public long value { get; set; }
        public string relation { get; set; }
    }

    public class Source
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

    public class Hits
    {
        [Ignore]
        public string _index { get; set; }


        [Ignore]
        public string _type { get; set; }

        [Ignore]
        public string _id { get; set; }

        [Ignore]
        public string _score { get; set; }

        public Source _source { get; set; }

        [Ignore]
        public List<string> sort { get; set; }

        [Ignore]
        public Total total { get; set; }

        [Ignore]
        public string max_score { get; set; }

        [Ignore]
        public List<Hits> hits { get; set; }
    }

    public class Root
    {
        public int? took { get; set; }
        public bool timed_out { get; set; }
        public Shards _shards { get; set; }
        public Hits hits { get; set; }
    }


}
