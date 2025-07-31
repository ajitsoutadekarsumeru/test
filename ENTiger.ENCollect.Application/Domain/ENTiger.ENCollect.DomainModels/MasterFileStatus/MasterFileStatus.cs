using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class MasterFileStatus : DomainModelBridge
    {
        protected readonly ILogger<MasterFileStatus> _logger;

        protected MasterFileStatus()
        {
        }

        public MasterFileStatus(ILogger<MasterFileStatus> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        public DateTime? FileProcessedDateTime { get; set; }

        public DateTime? FileUploadedDate { get; set; }

        [StringLength(300)]
        public string? Status { get; set; }

        [StringLength(100)]
        public string? UploadType { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion

    }
}