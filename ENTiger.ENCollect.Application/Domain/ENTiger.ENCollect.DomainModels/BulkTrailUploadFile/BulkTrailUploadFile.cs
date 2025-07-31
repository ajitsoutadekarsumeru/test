using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BulkTrailUploadFile : DomainModelBridge
    {
        protected readonly ILogger<BulkTrailUploadFile> _logger;

        protected BulkTrailUploadFile()
        {
        }

        public BulkTrailUploadFile(ILogger<BulkTrailUploadFile> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public void UpdateStatus(string status)
        {
            Status = status;
        }

        #endregion "Public"

        #region "Protected"

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        public DateTime FileUploadedDate { get; set; }

        public DateTime FileProcessedDateTime { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(200)]
        public string? MD5Hash { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}