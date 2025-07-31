using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Address : DomainModelBridge
    {
        protected readonly ILogger<Address> _logger;

        protected Address()
        {
        }

        public Address(ILogger<Address> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(200)]
        public string? AddressLine { get; set; }        

        [StringLength(100)]
        public string? LandMark { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? PIN { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}