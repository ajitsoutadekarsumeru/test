using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetWaiversByIdDto : DtoBridge
    {
        public List<WaiverDetailDto> WaiverDetails { get; set; }
    }
}
