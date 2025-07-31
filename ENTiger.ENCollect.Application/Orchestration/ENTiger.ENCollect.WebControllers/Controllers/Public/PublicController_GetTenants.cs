using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class PublicController : FlexControllerBridge<PublicController>
    {
        [HttpPost]
        [Route("Account/GetTenant")]
        [ProducesResponseType(typeof(IEnumerable<GetTenantsDto>), 200)]
        public async Task<IActionResult> GetTenants([FromBody] GetTenantsParams parameters)
        {
            return await RunQueryListServiceAsync<GetTenantsParams, GetTenantsDto>(parameters, _processPublicService.GetTenants);
        }
    }
}