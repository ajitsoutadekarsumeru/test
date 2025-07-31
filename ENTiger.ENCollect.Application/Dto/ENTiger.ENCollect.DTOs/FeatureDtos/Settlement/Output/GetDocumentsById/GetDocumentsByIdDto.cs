using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetDocumentsByIdDto : DtoBridge
    {
        public List<DocumentsDto> Documents { get; set; }
    }
}
