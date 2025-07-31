using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetInstallmentsByIdDto : DtoBridge
    {
        public List<InstallmentDetailDto> Installments { get; set; }
    }
}
