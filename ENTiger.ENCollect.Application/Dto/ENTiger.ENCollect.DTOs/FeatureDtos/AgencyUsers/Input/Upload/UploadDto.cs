using Microsoft.AspNetCore.Http;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class UploadDto : DtoBridge
    {
        public IFormFile file { get; set; }
    }
}