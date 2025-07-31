using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserPlaceOfWork : DomainModelBridge
    {
        protected readonly ILogger<CompanyUserPlaceOfWork> _logger;

        protected CompanyUserPlaceOfWork()
        {
        }

        public CompanyUserPlaceOfWork(ILogger<CompanyUserPlaceOfWork> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? PIN { get; set; }

        [StringLength(32)]
        public string? CompanyUserId { get; set; }

        public CompanyUser CompanyUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}