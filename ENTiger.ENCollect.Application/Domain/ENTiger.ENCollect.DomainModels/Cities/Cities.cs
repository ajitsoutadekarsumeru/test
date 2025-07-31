using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Cities : DomainModelBridge
    {
        protected readonly ILogger<Cities> _logger;

        protected Cities()
        {
        }

        public Cities(ILogger<Cities> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public string Name { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        //public ICollection<Area> Areas { get; set; }
        public State State { get; set; }

        [StringLength(32)]
        public string StateId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}