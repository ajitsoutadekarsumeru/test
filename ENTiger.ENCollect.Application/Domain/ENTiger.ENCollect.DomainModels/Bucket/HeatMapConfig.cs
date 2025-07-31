using ENTiger.ENCollect.DomainModels;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("HeatMapConfig")]
    public partial class HeatMapConfig : PersistenceModelBridge
    {
        protected readonly ILogger<HeatMapConfig> _logger;

        public HeatMapConfig()
        {
        }

        public HeatMapConfig(ILogger<HeatMapConfig> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        #endregion "Public"

        #region "Protected"

        [StringLength(100)]
        public string? HeatMapType { get; set; }


        [StringLength(32)]
        public string? RowId { get; set; }

        [StringLength(32)]
        public string? ColumnId { get; set; }

        public int RangeFrom { get; set; }

        public int RangeTo { get; set; }

        [StringLength(30)]
        public HeatIndicatorEnum HeatIndicator { get; set; }


        #endregion "Protected"

        #region "Private"
        #endregion "Private"

        #endregion "Attributes"

        #region "Private Methods"
        public Bucket disableBucket(Bucket bucket, string userId)
        {
            this.IsDeleted = true;
            this.LastModifiedBy = userId;
            return bucket;
        }
        #endregion "Private Methods"

        #region "Protected"
        //public string? Id { get; protected set; }

        //public string? Name { get; protected set; }

        //public string? Description { get; protected set; }

        #endregion "Protected"
    }
}