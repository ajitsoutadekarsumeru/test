using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpDelete()]
        [Route("communication/template/delete/{id}")]
        [Authorize(Policy = "CanDeleteCommunicationTemplatePolicy")]
        public async Task<IActionResult> DeleteTemplate(string Id)
        {
            DeleteTemplateDto dto = new DeleteTemplateDto();
            dto.Id = Id;
            var result = RateLimit(dto, "delete_communication_template");
            return result ?? await RunService(200, dto, _processCommunicationService.DeleteTemplate);
        }
    }
}