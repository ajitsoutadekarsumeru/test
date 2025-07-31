using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> DeactivateAgency(DeactivateAgencyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DeactivateAgencyDataPacket, DeactivateAgencySequence, DeactivateAgencyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DeactivateAgencyCommand cmd = new DeactivateAgencyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DeactivateAgencyResultModel outputResult = new DeactivateAgencyResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DeactivateAgencyResultModel : DtoBridge
    {
    }
}