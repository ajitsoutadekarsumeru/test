using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryUnAllocationFile : DomainModelBridge
    {
        protected readonly ILogger<SecondaryUnAllocationFile> _logger;

        protected PrimaryUnAllocationFile()
        {
        }

        public PrimaryUnAllocationFile(ILogger<SecondaryUnAllocationFile> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(100)]
        public string? UploadType { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string? Status { get; protected set; }

        public DateTime UploadedDate { get; set; }

        public DateTime? ProcessedDateTime { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        public void UpdateStatus(string status)
        {
            Status = status;
        }
    }
}