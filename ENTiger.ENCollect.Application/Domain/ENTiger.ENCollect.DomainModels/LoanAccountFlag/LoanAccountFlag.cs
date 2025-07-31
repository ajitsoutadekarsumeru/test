using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountFlag : DomainModelBridge
    {
        protected readonly ILogger<LoanAccountFlag> _logger;

        protected LoanAccountFlag()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<LoanAccountFlag>>();
        }

        public LoanAccountFlag(ILogger<LoanAccountFlag> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public LoanAccount LoanAccount { get; set; }
        public string LoanAccountId { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}