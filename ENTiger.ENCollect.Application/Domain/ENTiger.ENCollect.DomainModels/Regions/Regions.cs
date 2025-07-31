using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Regions : DomainModelBridge
    {
        protected readonly ILogger<Regions> _logger;

        protected Regions()
        {
        }

        public Regions(ILogger<Regions> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public string Name { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        public ICollection<State> States { get; set; }
        public Countries Country { get; set; }

        [StringLength(32)]
        public string CountryId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}