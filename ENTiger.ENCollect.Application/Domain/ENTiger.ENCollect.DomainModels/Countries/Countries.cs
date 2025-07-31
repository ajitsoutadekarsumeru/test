using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Countries : DomainModelBridge
    {
        protected readonly ILogger<Countries> _logger;

        protected Countries()
        {
        }

        public Countries(ILogger<Countries> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        public ICollection<Regions> Regions { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}