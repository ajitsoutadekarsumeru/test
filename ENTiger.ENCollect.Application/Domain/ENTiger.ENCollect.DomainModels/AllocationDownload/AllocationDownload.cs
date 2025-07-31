using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AllocationDownload : DomainModelBridge
    {
        protected readonly ILogger<AllocationDownload> _logger;

        public AllocationDownload()
        {
        }

        public AllocationDownload(ILogger<AllocationDownload> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(1000)]
        public string? InputJson { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(500)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(50)]
        public string? AllocationType { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}