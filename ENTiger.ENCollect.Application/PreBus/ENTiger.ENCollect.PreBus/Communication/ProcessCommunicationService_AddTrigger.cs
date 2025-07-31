using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddTrigger(AddTriggerDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddTriggerDataPacket, AddTriggerSequence, AddTriggerDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddTriggerCommand cmd = new AddTriggerCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddTriggerResultModel outputResult = new AddTriggerResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class AddTriggerResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
