using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Sequence : DomainModelBridge
    {
        protected readonly ILogger<Sequence> _logger;

        protected Sequence()
        {
        }

        public Sequence(ILogger<Sequence> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string Name { get; set; }

        public int Value { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}