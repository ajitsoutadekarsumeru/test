using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    public partial class UserSearchCriteriaController : FlexControllerBridge<UserSearchCriteriaController>
    {
        [HttpGet()]
        [Route("getfilterdetail/{Id}")]
        [ProducesResponseType(typeof(GetByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string Id)
        {
            GetByIdParams parameters = new GetByIdParams();
            parameters.Id = Id;

            return await RunQuerySingleServiceAsync<GetByIdParams, GetByIdDto>(
                        parameters, _processUserSearchCriteriaService.GetById);
        }
    }
}