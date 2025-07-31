using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    ///
    [Table("Languages")]
    public partial class Language : DomainModelBridge
    {
        protected readonly ILogger<Language> _logger;

        protected Language()
        {
        }

        public Language(ILogger<Language> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(32)]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}