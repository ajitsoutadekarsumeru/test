using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpGet()]
        [Route("collectionstaff/list/{isFrontEnd}")]
        [ProducesResponseType(typeof(IEnumerable<GetListsDto>), 200)]
        public async Task<IActionResult> GetList(bool isFrontEnd)
        {
            GetListParams parameters = new GetListParams();
            parameters.IsFrontEnd = isFrontEnd;
            return await RunQueryListServiceAsync<GetListParams, GetListsDto>(
                        parameters, _processCompanyUsersService.GetList);
        }
    }
}