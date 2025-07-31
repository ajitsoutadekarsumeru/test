using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> Upload(UploadDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UploadDataPacket, UploadSequence, UploadDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                packet.OutputDto.SetAppContext(dto.GetAppContext());
                UploadCommand cmd = new UploadCommand
                {
                    Dto = packet.OutputDto
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UploadResultModel outputResult = new UploadResultModel()
                {
                    DocumentId = this.Id,
                    FileName = packet.OutputDto.Name,
                    filePath = packet.OutputDto.Path,
                    FileSize = packet.OutputDto.Size
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UploadResultModel : DtoBridge
    {
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public string? filePath { get; set; }
        public string? DocumentId { get; set; }
    }
}