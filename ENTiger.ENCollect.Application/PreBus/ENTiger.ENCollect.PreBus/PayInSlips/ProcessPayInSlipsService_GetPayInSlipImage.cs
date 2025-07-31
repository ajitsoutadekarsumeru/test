using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetPayInSlipImage(GetPayInSlipImageDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetPayInSlipImageDataPacket, GetPayInSlipImageSequence, GetPayInSlipImageDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetPayInSlipImageCommand cmd = new GetPayInSlipImageCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = dto.FileName;
                return cmdResult;
            }
        }
    }

    public class GetPayInSlipImageResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}