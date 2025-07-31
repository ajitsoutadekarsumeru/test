using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class PublicController : FlexControllerBridge<PublicController>
    {
        [HttpPost]
        [Route("mvp/import/collections")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ImportCollections([FromBody]ImportCollectionsDto dto)
        {
            return await RunService(201, dto, _processPublicService.ImportCollections);
        }
    }
}
