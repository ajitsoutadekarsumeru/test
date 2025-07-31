using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CollectionUploadFile : DomainModelBridge
    {
        protected readonly ILogger<CollectionUploadFile> _logger;

        protected CollectionUploadFile()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<CollectionUploadFile>>();
        }

        public CollectionUploadFile(ILogger<CollectionUploadFile> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        #endregion

        #region "Protected"
        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        public DateTime FileUploadedDate { get; set; }

        public DateTime FileProcessedDateTime { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }
        [StringLength(50)]
        public string? UploadType { get; set; }
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion

    }
}
