using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetProfileImage(GetProfileImageDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetProfileImageDataPacket, GetProfileImageSequence, GetProfileImageDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetProfileImageCommand cmd = new GetProfileImageCommand
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

    public class GetProfileImageResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}