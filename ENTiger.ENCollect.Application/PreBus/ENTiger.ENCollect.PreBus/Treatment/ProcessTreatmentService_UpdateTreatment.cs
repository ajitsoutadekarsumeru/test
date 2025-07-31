using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateTreatment(UpdateTreatmentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateTreatmentDataPacket, UpdateTreatmentSequence, UpdateTreatmentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateTreatmentCommand cmd = new UpdateTreatmentCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateTreatmentResultModel outputResult = new UpdateTreatmentResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateTreatmentResultModel : DtoBridge
    {
    }
}