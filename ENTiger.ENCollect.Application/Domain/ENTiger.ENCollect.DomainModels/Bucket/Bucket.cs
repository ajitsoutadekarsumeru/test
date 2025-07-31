using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("Buckets")]
    public partial class Bucket : DomainModelBridge
    {
        protected readonly ILogger<Bucket> _logger;

        public Bucket()
        {
        }

        public Bucket(ILogger<Bucket> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        #endregion "Public"

        #region "Protected"
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? DisplayLabel { get; set; }
        public int DPD_From { get; set; }

        public int DPD_To { get; set; }


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