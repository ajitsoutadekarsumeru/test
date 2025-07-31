using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("agency/{id}")]
        [Authorize(Policy = "CanViewAgencyPolicy")]
        [ProducesResponseType(typeof(CollectionAgencyDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CollectionAgencyDetails(string id)
        {
            CollectionAgencyDetailsParams parameters = new CollectionAgencyDetailsParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<CollectionAgencyDetailsParams, CollectionAgencyDetailsDto>(
                        parameters, _processAgencyService.CollectionAgencyDetails);
        }
    }
}