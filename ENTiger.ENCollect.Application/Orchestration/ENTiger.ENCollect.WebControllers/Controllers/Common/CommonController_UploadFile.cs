using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            UploadFileDto dto = new UploadFileDto() { file = file };
            var result = RateLimit(dto, "upload_file");
            return result ?? await RunService(201, dto, _processCommonService.UploadFile);
        }
    }
}