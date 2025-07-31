using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class AzureLoginDto : DtoBridge
    {
        [Required]
        public string ReferenceId { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}