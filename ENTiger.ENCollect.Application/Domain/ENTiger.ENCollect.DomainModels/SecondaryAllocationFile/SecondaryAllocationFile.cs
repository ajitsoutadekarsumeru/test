using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationFile : DomainModelBridge
    {
        protected readonly ILogger<SecondaryAllocationFile> _logger;

        protected SecondaryAllocationFile()
        {
        }

        public SecondaryAllocationFile(ILogger<SecondaryAllocationFile> logger)
        {
            _logger = logger;
        }

        public SecondaryAllocationFile(string fileName, string description)
        {
            this.CreatedDate = DateTime.Now;
            this.FileUploadedDate = DateTime.Now;
            this.FileProcessedDateTime = DateTime.Now;
            this.FileName = fileName;
            this.Description = description; // "Agency Allocation Dump";
        }

        #region "Attributes"

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(100)]
        public string? UploadType { get; set; }

        public DateTime FileUploadedDate { get; set; }
        public DateTime FileProcessedDateTime { get; set; }

        #region "Public"

        public void UpdateStatus(string status)
        {
            this.Status = status;
        }

        #endregion "Public"

        #endregion "Attributes"
    }
}