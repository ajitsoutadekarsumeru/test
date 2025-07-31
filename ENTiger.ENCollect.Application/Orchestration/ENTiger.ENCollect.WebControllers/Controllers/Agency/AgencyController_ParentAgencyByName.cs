using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("agency/parent/byname/{name}")]
        [ProducesResponseType(typeof(IEnumerable<ParentAgencyByNameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ParentAgencyByName(string name)
        {
            ParentAgencyByNameParams parameters = new ParentAgencyByNameParams();
            parameters.name = name;

            return await RunQueryListServiceAsync<ParentAgencyByNameParams, ParentAgencyByNameDto>(
                        parameters, _processAgencyService.ParentAgencyByName);
        }
    }
}