using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    public partial class UserSearchCriteriaController : FlexControllerBridge<UserSearchCriteriaController>
    {
        [HttpPost]
        [Route("add/FilterValues")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> Add([FromBody] AddDto dto)
        {
            var result = RateLimit(dto, "add_filter_values");
            return result ?? await RunService(201, dto, _processUserSearchCriteriaService.Add);
        }
    }
}