using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UsersCreateFileDtoWithId : UsersCreateFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }

        public string? FileName { get; set; }

        public string? CustomId { get; set; }

        public string? UploadType { get; set; }
    }
}