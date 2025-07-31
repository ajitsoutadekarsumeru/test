using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Segmentation : DomainModelBridge
    {
        protected readonly ILogger<Segmentation> _logger;

        protected Segmentation()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<Segmentation>>();
        }

        public Segmentation(ILogger<Segmentation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(20)]
        public string? ExecutionType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? ProductGroup { get; set; }

        [StringLength(500)]
        public string? Product { get; set; }

        [StringLength(500)]
        public string? SubProduct { get; set; }

        [StringLength(50)]
        public string? BOM_Bucket { get; set; }

        [StringLength(50)]
        public string? CurrentBucket { get; set; }

        [StringLength(100)]
        public string? NPA_Flag { get; set; }

        [StringLength(100)]
        public string? Current_DPD { get; set; }

        [StringLength(100)]
        public string? Zone { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(500)]
        public string? Branch { get; set; }

        public int? Sequence { get; set; }

        public bool? IsDisabled { get; set; }

        [StringLength(32)]
        public string? SegmentAdvanceFilterId { get; set; }
        public SegmentationAdvanceFilter SegmentAdvanceFilter { get; set; }

        public string? ClusterName { get; set; }

        #endregion "Public"

        #region "Protected"
        #endregion "Protected"

        #region "Private"
        #endregion "Private"

        #endregion "Attributes"

        #region "Private Methods"
        #endregion "Private Methods"
    }
}