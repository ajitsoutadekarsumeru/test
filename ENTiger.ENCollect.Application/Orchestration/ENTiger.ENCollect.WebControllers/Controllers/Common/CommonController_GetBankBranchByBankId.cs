using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/branchlist/{Id}")]
        [ProducesResponseType(typeof(IEnumerable<GetBankBranchByBankIdDto>), 200)]
        public async Task<IActionResult> GetBankBranchByBankId(string Id)
        {
            GetBankBranchByBankIdParams parameters = new GetBankBranchByBankIdParams();
            parameters.Id = Id;
            return await RunQueryListServiceAsync<GetBankBranchByBankIdParams, GetBankBranchByBankIdDto>(parameters, _processCommonService.GetBankBranchByBankId);
        }
    }
}