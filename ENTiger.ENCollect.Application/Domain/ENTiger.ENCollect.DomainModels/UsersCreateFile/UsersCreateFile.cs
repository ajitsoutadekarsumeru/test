using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UsersCreateFile : DomainModelBridge
    {
        protected readonly ILogger<UsersCreateFile> _logger;

        protected UsersCreateFile()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<UsersCreateFile>>();
        }

        public UsersCreateFile(ILogger<UsersCreateFile> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string CustomId { get; set; }

        [StringLength(100)]
        public string UploadType { get; set; }

        [StringLength(250)]
        public string FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime UploadedDate { get; set; }

        public DateTime? ProcessedDateTime { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        public void UpdateStatus(string status)
        {
            Status = status;
        }
    }
}