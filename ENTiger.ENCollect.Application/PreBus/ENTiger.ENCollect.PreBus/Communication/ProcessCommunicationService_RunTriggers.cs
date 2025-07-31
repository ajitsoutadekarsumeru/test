using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> RunTriggers(RunTriggersDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RunTriggersDataPacket, RunTriggersSequence, RunTriggersDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RunTriggersCommand cmd = new RunTriggersCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RunTriggersResultModel outputResult = new RunTriggersResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class RunTriggersResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
