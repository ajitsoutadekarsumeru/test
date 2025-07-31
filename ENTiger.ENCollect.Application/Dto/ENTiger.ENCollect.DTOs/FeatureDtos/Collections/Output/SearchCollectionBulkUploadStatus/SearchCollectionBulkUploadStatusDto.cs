using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchCollectionBulkUploadStatusDto : DtoBridge
    {
        public string? TransactionId { get; set; }
        public string? FileName { get; set; }
        public string? Status { get; set; }
        public string? DownloadFileName { get; set; }
    }
}
