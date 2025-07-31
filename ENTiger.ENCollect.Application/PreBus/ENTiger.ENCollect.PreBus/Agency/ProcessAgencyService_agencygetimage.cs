using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> agencygetimage(agencygetimageDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<agencygetimageDataPacket, agencygetimageSequence, agencygetimageDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //    dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //    agencygetimageCommand cmd = new agencygetimageCommand
                //    {
                //         Dto = dto,
                //    };
                //    await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = dto.FileName;
                return cmdResult;
            }
        }
    }

    public class agencygetimageResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}