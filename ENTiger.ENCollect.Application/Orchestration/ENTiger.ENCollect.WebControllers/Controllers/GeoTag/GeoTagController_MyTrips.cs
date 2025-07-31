using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        [HttpPost()]
        [Route("mytrips")]
        [Authorize(Policy = "CanSearchMyTripsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<MyTripsDto>), 200)]
        public async Task<IActionResult> MyTrips([FromBody] MyTripsParams parameters)
        {
            return await RunQueryListServiceAsync<MyTripsParams, MyTripsDto>(
                        parameters, _processGeoTagService.MyTrips);
        }
    }
}