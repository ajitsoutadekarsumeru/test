using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TrailIntensityDownload : DomainModelBridge
    {
        protected readonly ILogger<TrailIntensityDownload> _logger;

        protected TrailIntensityDownload()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TrailIntensityDownload>>();
        }

        public TrailIntensityDownload(ILogger<TrailIntensityDownload> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(1000)]
        public string? InputJson { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(500)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}