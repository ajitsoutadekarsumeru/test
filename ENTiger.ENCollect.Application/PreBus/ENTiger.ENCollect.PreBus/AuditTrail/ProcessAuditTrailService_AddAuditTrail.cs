using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AuditTrailModule
{
    public partial class ProcessAuditTrailService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddAuditTrail(AddAuditTrailDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddAuditTrailDataPacket, AddAuditTrailSequence, AddAuditTrailDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddAuditTrailCommand cmd = new AddAuditTrailCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddAuditTrailResultModel outputResult = new AddAuditTrailResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class AddAuditTrailResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
