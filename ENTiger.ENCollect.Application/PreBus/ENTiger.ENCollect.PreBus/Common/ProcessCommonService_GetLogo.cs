using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetLogo(GetLogoDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetLogoDataPacket, GetLogoSequence, GetLogoDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetLogoCommand cmd = new GetLogoCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = packet.Logo;
                return cmdResult;
            }
        }
    }

    public class GetLogoResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}