using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("Resolutions")]
    public partial class Resolution : PersistenceModelBridge
    {
        protected readonly ILogger<Resolution> _logger;

        public Resolution()
        {
        }

        public Resolution(ILogger<Resolution> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        #endregion "Public"

        #region "Protected"
        public ResolutionTypeEnum Code { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }
        #endregion "Protected"

        #region "Private"
        #endregion "Private"

        #endregion "Attributes"

        #region "Private Methods"
      
        #endregion "Private Methods"

        #region "Protected"
        //public string? Id { get; protected set; }

        //public string? Name { get; protected set; }

        //public string? Description { get; protected set; }

        #endregion "Protected"
    }
}