using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpGet()]
        [Route("communication/template/{id}")]
        [Authorize(Policy = "CanViewCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(GetTemplateByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTemplateById(string id)
        {
            GetTemplateByIdParams parameters = new GetTemplateByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetTemplateByIdParams, GetTemplateByIdDto>(
                        parameters, _processCommunicationService.GetTemplateById);
        }
    }
}