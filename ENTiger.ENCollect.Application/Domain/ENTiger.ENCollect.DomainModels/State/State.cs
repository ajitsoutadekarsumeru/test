using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class State : DomainModelBridge
    {
        protected readonly ILogger<State> _logger;

        protected State()
        {
        }

        public State(ILogger<State> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public string? Name { get; set; }

        [StringLength(50)]
        public string? NickName { get; set; }

        [StringLength(100)]
        public string? PrimaryLanguage { get; set; }

        [StringLength(100)]
        public string? SecondaryLanguage { get; set; }

        public ICollection<Cities> Cities { get; set; }
        public Regions? Region { get; set; }

        [StringLength(32)]
        public string? RegionId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}