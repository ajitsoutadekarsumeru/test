using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateTemplate(UpdateTemplateDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateTemplateDataPacket, UpdateTemplateSequence, UpdateTemplateDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateTemplateCommand cmd = new UpdateTemplateCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateTemplateResultModel outputResult = new UpdateTemplateResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateTemplateResultModel : DtoBridge
    {
    }
}