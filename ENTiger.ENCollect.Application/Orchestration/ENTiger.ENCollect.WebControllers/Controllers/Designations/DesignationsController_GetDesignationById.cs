using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("get/designationById/{id}")]
        [ProducesResponseType(typeof(GetDesignationByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDesignationById(string id)
        {
            GetDesignationByIdParams parameters = new GetDesignationByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetDesignationByIdParams, GetDesignationByIdDto>(
                        parameters, _processDesignationsService.GetDesignationById);
        }
    }
}