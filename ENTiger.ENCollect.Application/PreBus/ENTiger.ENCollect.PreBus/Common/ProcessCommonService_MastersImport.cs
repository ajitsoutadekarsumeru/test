using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> MastersImport(MastersImportDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<MastersImportDataPacket, MastersImportSequence, MastersImportDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                MastersImportCommand cmd = new MastersImportCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                MastersImportResultModel outputResult = new MastersImportResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.CustomId = dto.CustomId;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class MastersImportResultModel : DtoBridge
    {
        public string Id { get; set; }

        public string? CustomId { get; set; }
    }
}