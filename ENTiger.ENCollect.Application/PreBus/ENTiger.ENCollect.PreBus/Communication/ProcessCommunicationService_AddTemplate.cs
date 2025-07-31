using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddTemplate(AddTemplateDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddTemplateDataPacket, AddTemplateSequence, AddTemplateDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddTemplateCommand cmd = new AddTemplateCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddTemplateResultModel outputResult = new AddTemplateResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddTemplateResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}