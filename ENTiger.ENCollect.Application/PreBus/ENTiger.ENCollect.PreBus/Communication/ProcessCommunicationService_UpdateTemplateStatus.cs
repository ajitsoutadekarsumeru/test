using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateTemplateStatus(UpdateTemplateStatusDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateTemplateStatusDataPacket, UpdateTemplateStatusSequence, UpdateTemplateStatusDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateTemplateStatusCommand cmd = new UpdateTemplateStatusCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateTemplateStatusResultModel outputResult = new UpdateTemplateStatusResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateTemplateStatusResultModel : DtoBridge
    {
    }
}