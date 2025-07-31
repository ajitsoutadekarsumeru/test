using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyReceiptsSummaryDto : DtoBridge
    {
        public int TotalReceiptsCount { get; set; }
        public decimal? TotalReceiptAmount { get; set; }
    }
}
