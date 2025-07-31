using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet]
        [Route("treatment/reports/download")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetTreatmentReportFile(string fileName)
        {
            GetTreatmentReportFileDto dto = new GetTreatmentReportFileDto();
            dto.FileName = fileName;
            return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
        }
    }
}