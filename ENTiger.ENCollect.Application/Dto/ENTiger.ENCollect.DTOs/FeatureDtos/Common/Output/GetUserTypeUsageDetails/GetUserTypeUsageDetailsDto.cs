using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUserTypeUsageDetailsDto : DtoBridge
    {
        public int CurrentConsumption { get; set; }

        public int Limit { get; set; }

        public decimal PercentUsed { get; set; }
        public string? ColourCode { get; set; }
    }
}
