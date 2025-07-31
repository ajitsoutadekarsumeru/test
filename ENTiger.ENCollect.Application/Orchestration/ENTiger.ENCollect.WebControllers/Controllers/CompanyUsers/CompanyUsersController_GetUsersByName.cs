using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpGet()]
        [Route("collectionstaff/byname/{name}")]
        [ProducesResponseType(typeof(IEnumerable<GetUserByNameDto>), 200)]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            GetUsersByNameParams parameters = new GetUsersByNameParams();
            parameters.Name = name;
            return await RunQueryListServiceAsync<GetUsersByNameParams, GetUserByNameDto>(
                        parameters, _processCompanyUsersService.GetUsersByName);
        }
    }
}