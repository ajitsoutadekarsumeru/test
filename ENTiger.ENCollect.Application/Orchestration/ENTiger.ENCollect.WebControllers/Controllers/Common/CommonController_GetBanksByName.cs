using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/banks/byname/{name}")]
        [ProducesResponseType(typeof(IEnumerable<GetBanksByNameDto>), 200)]
        public async Task<IActionResult> GetBanksByName(string name)
        {
            GetBanksByNameParams parameters = new GetBanksByNameParams();
            parameters.Name = name;
            return await RunQueryListServiceAsync<GetBanksByNameParams, GetBanksByNameDto>(
                        parameters, _processCommonService.GetBanksByName);
        }
    }
}