using Microsoft.AspNetCore.Http;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class UploadFileDto : DtoBridge
    {
        public IFormFile file { get; set; }
    }
}