using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> DeleteTemplate(DeleteTemplateDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DeleteTemplateDataPacket, DeleteTemplateSequence, DeleteTemplateDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DeleteTemplateCommand cmd = new DeleteTemplateCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DeleteTemplateResultModel outputResult = new DeleteTemplateResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DeleteTemplateResultModel : DtoBridge
    {
    }
}