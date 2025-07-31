using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserPlaceOfWork : DomainModelBridge
    {
        protected readonly ILogger<AgencyUserPlaceOfWork> _logger;

        protected AgencyUserPlaceOfWork()
        {
        }

        public AgencyUserPlaceOfWork(ILogger<AgencyUserPlaceOfWork> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? PIN { get; set; }

        [StringLength(32)]
        public string? AgencyUserId { get; set; }

        public AgencyUser AgencyUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}