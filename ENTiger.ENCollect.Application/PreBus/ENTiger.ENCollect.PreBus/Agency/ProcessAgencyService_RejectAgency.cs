using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RejectAgency(RejectAgencyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RejectAgencyDataPacket, RejectAgencySequence, RejectAgencyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RejectAgencyCommand cmd = new RejectAgencyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RejectAgencyResultModel outputResult = new RejectAgencyResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RejectAgencyResultModel : DtoBridge
    {
    }
}