using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> ExecuteTreatment(ExecuteTreatmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ExecuteTreatmentDataPacket, ExecuteTreatmentSequence, ExecuteTreatmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ExecuteTreatmentCommand cmd = new ExecuteTreatmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ExecuteTreatmentResultModel outputResult = new ExecuteTreatmentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ExecuteTreatmentResultModel : DtoBridge
    {
    }
}