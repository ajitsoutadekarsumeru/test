using Sumeru.Flex;
using System;
using System.Xml.Linq;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMoneyMovementSummaryDto : DtoBridge
    {
       public string? agencyName { get; set; }
        public string? agentName {get; set; }

        public string? agencyId { get; set; }
        public string? agentId { get; set; }

        public string? productGroup { get; set; }
        public string? product { get; set; }
        public string? subProduct { get; set; }
        public int? currentBucket { get; set; }
        public string? bomBucket { get; set; }
        public string? region { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public string? branchName { get; set; }

        public string? collectionDate { get; set; }
        public string? depositDate { get; set; }

        public string? paymentMode { get; set; }
        public string? paymentStatus { get; set; }
        public long? countOfCollectedAccounts { get; set; }

        public decimal? collectedAmount { get; set; }
        public string? category { get; set; }
        public string? daysBucket { get; set; }
        public string? holdDays { get; set; }
        public string? loanAmountBucket { get; set; }

        //public string? staffOrAgent { get; set; }


    }
}
