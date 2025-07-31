using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPut]
        [Route("companyuser/activate")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CompanyUsersActivate([FromBody] CompanyUsersActivateDto dto)
        {
            return await RunService(200, dto, _processCompanyUsersService.CompanyUsersActivate);
        }
    }
}
