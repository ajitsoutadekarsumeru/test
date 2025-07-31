using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> EditCollectionAgency(EditCollectionAgencyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<EditCollectionAgencyDataPacket, EditCollectionAgencySequence, EditCollectionAgencyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                EditCollectionAgencyCommand cmd = new EditCollectionAgencyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                EditCollectionAgencyResultModel outputResult = new EditCollectionAgencyResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class EditCollectionAgencyResultModel : DtoBridge
    {
    }
}