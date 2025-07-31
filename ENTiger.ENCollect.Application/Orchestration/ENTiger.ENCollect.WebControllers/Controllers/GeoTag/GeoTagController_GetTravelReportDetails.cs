using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        [HttpPost()]
        [Route("geotagtravelreport")]
        [Authorize(Policy = "CanSearchTravelReportPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetTravelReportDetailsDto>), 200)]
        public async Task<IActionResult> GetTravelReportDetails([FromBody] GetTravelReportDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetTravelReportDetailsParams, GetTravelReportDetailsDto>(parameters, _processGeoTagService.GetTravelReportDetails);
        }
    }
}