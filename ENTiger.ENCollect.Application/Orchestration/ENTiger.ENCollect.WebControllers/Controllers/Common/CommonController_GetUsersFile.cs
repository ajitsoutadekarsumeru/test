using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/users/getfile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetUsersFile([FromBody] GetUsersFileDto dto)
        {
            var result = await RunService(200, dto, _processCommonService.GetUsersFile);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName, (result as ObjectResult).Value.ToString());
            }
            return result;
        }
    }
}