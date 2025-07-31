using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UploadFile(UploadFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UploadFileDataPacket, UploadFileSequence, UploadFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //UploadFileCommand cmd = new UploadFileCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                cmdResult.result = packet.OutputDto;
                return cmdResult;
            }
        }
    }

    public class UploadFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}