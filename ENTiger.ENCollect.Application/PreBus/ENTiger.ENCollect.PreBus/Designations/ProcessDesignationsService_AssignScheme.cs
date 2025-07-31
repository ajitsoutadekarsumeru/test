using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class ProcessDesignationsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AssignScheme(AssignSchemeDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AssignSchemeDataPacket, AssignSchemeSequence, AssignSchemeDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AssignSchemeCommand cmd = new AssignSchemeCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AssignSchemeResultModel outputResult = new AssignSchemeResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class AssignSchemeResultModel : DtoBridge
    {
    }
}
