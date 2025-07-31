using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CategoryMaster : DomainModelBridge
    {
        protected readonly ILogger<CategoryMaster> _logger;

        protected CategoryMaster()
        {
        }

        public CategoryMaster(ILogger<CategoryMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(250)]
        public string Name { get; protected set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}