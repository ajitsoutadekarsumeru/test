using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RenewAgency(RenewAgencyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RenewAgencyDataPacket, RenewAgencySequence, RenewAgencyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RenewAgencyCommand cmd = new RenewAgencyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RenewAgencyResultModel outputResult = new RenewAgencyResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RenewAgencyResultModel : DtoBridge
    {
    }
}