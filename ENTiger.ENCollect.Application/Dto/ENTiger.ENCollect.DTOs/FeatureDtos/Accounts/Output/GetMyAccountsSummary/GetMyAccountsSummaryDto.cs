using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyAccountsSummaryDto : DtoBridge
    { // My Accounts Summary
      // My Accounts Summary
        public int TotalTrailsCount { get; set; }
        public int TotalAllocationCount { get; set; }
        public decimal TotalAllocationPOS { get; set; }
        public decimal TotalAllocationTAD { get; set; }

        public int CollectedCount { get; set; }
        public decimal CollectedPOS { get; set; }
        public decimal CollectedTAD { get; set; }

        public int AttemptedCount { get; set; }
        public decimal AttemptedPOS { get; set; }
        public decimal AttemptedTAD { get; set; }

        public int UnAttemptedCount { get; set; }
        public decimal UnAttemptedPOS { get; set; }
        public decimal UnAttemptedTAD { get; set; }

        // My Attempted Accounts
        public int Attempted_PTP_Count { get; set; }
        public decimal Attempted_PTP_POS { get; set; }
        public decimal Attempted_PTP_TAD { get; set; }

        public int Attempted_Contacted_Count { get; set; }
        public decimal Attempted_Contacted_POS { get; set; }
        public decimal Attempted_Contacted_TAD { get; set; }

        public int Attempted_NoContact_Count { get; set; }
        public decimal Attempted_NoContact_POS { get; set; }
        public decimal Attempted_NoContact_TAD { get; set; }

        public int Attempted_Others_Count { get; set; }
        public decimal Attempted_Others_POS { get; set; }
        public decimal Attempted_Others_TAD { get; set; }
        public int RollBackCount { get; set; }
        public decimal RollBackPOS { get; set; }
        public decimal RollBackTAD { get; set; }

        public int StabilizedCount { get; set; }
        public decimal StabilizedPOS { get; set; }
        public decimal StabilizedTAD { get; set; }

        public int NormalizedCount { get; set; }
        public decimal NormalizedPOS { get; set; }
        public decimal NormalizedTAD { get; set; }

        public int RollForwardCount { get; set; }
        public decimal RollForwardPOS { get; set; }
        public decimal RollForwardTAD { get; set; }

        public int TotalBrokenPTPCount { get; set; }

        // Optional: Percentage Calculations (UI/Report level)
        public decimal TotalAllocationPercentage => 100m;
        public decimal AttemptedPercentage => TotalAllocationCount > 0 ? Math.Round(((decimal)AttemptedCount / TotalAllocationCount) * 100, 2) : 0m;
        public decimal CollectedPercentage => TotalAllocationCount > 0 ? Math.Round(((decimal)CollectedCount / TotalAllocationCount) * 100, 2) : 0m;
        public decimal UnAttemptedPercentage => TotalAllocationCount > 0 ? Math.Round(((decimal)UnAttemptedCount / TotalAllocationCount) * 100, 2) : 0m;

        public decimal PtpPercentage => AttemptedCount > 0 ? Math.Round(((decimal)Attempted_PTP_Count / AttemptedCount) * 100, 2) : 0m;
        public decimal ContactedPercentage => AttemptedCount > 0 ? Math.Round(((decimal)Attempted_Contacted_Count / AttemptedCount) * 100, 2) : 0m;
        public decimal NoContactPercentage => AttemptedCount > 0 ? Math.Round(((decimal)Attempted_NoContact_Count / AttemptedCount) * 100, 2) : 0m;
        public decimal OtherPercentage => AttemptedCount > 0 ? Math.Round(((decimal)Attempted_Others_Count / AttemptedCount) * 100, 2) : 0m;
        public decimal RollBackPercentage => TotalAllocationCount > 0 ? Math.Round((RollBackCount * 100m) / TotalAllocationCount, 2) : 0m;
        public decimal StabilizedPercentage => TotalAllocationCount > 0 ? Math.Round((StabilizedCount * 100m) / TotalAllocationCount, 2) : 0m;
        public decimal NormalizedPercentage => TotalAllocationCount > 0 ? Math.Round((NormalizedCount * 100m) / TotalAllocationCount, 2) : 0m;
        public decimal RollForwardPercentage => TotalAllocationCount > 0 ? Math.Round((RollForwardCount * 100m) / TotalAllocationCount, 2) : 0m;

    }
    public class SummaryStats
    {
        public int Count { get; set; }
        public decimal POS { get; set; }
        public decimal TAD { get; set; }
    }


}
