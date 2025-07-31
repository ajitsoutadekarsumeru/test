using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> Ikontel(IkontelDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<IkontelDataPacket, IkontelSequence, IkontelDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                IkontelCommand cmd = new IkontelCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                IkontelResultModel outputResult = new IkontelResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class IkontelResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}