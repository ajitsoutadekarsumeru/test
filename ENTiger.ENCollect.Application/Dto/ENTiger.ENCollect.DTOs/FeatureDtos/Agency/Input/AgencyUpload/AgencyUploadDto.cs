using Microsoft.AspNetCore.Http;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyUploadDto : DtoBridge
    {
        public IFormFile file { get; set; }
    }
}