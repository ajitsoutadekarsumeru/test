using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.ReportsModule
{

    public partial class ReportsController : FlexControllerBridge<ReportsController>
    {
        [HttpGet()]
        [Route("reports/collection/intensity/details")]
        [ProducesResponseType(typeof(FlexiPagedList<GetCollectionIntensityDetailsDto>), 200)]
        public async Task<IActionResult> GetCollectionIntensityDetails([FromQuery]GetCollectionIntensityDetailsParams parameters)
        {
            return RunQueryPagedService<GetCollectionIntensityDetailsParams, GetCollectionIntensityDetailsDto>(parameters, _processReportsService.GetCollectionIntensityDetails);
        }

    }

    
}
