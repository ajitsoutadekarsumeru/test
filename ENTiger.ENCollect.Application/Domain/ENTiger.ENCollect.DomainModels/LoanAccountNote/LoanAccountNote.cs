using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountNote : DomainModelBridge
    {
        protected readonly ILogger<LoanAccountNote> _logger;

        protected LoanAccountNote()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<LoanAccountNote>>();
        }

        public LoanAccountNote(ILogger<LoanAccountNote> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public LoanAccount LoanAccount { get; set; }
        public string LoanAccountId { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}