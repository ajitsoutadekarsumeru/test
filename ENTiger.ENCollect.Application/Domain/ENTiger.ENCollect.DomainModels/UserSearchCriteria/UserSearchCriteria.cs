using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserSearchCriteria : DomainModelBridge
    {
        protected readonly ILogger<UserSearchCriteria> _logger;

        protected UserSearchCriteria()
        {
        }

        public UserSearchCriteria(ILogger<UserSearchCriteria> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public bool? isdisable { get; set; }

        public string FilterValues { get; set; }
        public string filterName { get; set; }
        public ApplicationUser User { get; set; }

        [StringLength(32)]
        public string UserId { get; set; }

        public string UseCaseName { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}