using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpGet()]
        [Route("communication/trigger/{id}")]
        [Authorize(Policy = "CanViewCommunicationTriggerPolicy")]
        [ProducesResponseType(typeof(GetTriggerByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTriggerById(string id)
        {
            GetTriggerByIdParams parameters = new GetTriggerByIdParams();
            parameters.Id = id;

            return RunQuerySingleService<GetTriggerByIdParams, GetTriggerByIdDto>(
                        parameters, _processCommunicationService.GetTriggerById);
        }
    }
}
