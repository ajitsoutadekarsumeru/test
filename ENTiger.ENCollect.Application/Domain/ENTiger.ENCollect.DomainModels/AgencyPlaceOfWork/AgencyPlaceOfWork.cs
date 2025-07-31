using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyPlaceOfWork : DomainModelBridge
    {
        protected readonly ILogger<AgencyPlaceOfWork> _logger;

        protected AgencyPlaceOfWork()
        {
        }

        public AgencyPlaceOfWork(ILogger<AgencyPlaceOfWork> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Product { get; set; }

        [StringLength(50)]
        public string? ProductGroup { get; set; }

        [StringLength(50)]
        public string? SubProduct { get; set; }

        [StringLength(50)]
        public string? Bucket { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? Region { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? Zone { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(32)]
        public string? ManagerId { get; set; }

        public CompanyUser Manager { get; set; }

        [StringLength(32)]
        public string? AgencyId { get; set; }

        public Agency Agency { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}